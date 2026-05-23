using System.Collections;
using UnityEngine;
using TMPro;

public class TextArchitect
{
    private TextMeshProUGUI _tmpro_ui;
    private TextMeshPro _tmpro_world;
    public TMP_Text tmpro => _tmpro_ui != null ? _tmpro_ui : _tmpro_world;

    public string _currentText => tmpro.text;
    public string _targetText { get; private set; } = ""; //le texte à afficher
    public string _preText { get; private set; } = "";
    public string _fullTargetText => _preText + _targetText;
    private int _preTextLength = 0;

    public enum BuildMethod { instant, typewriter, fade } //La méthode utilisée pour afficher peu à peu le texte
    private BuildMethod _buildMethod = BuildMethod.typewriter;
    public BuildMethod buildMethod
    {
        get { return _buildMethod; }
        set { _buildMethod = value; }
    }
    public Color _textColor { get { return tmpro.color; } set { tmpro.color = value; } } //Couleur du texte

    public float _speed { get { return _BASE_SPEED * _speedMultiplier; } set { _speedMultiplier = value; } }
    private const float _BASE_SPEED = 1f; //la vitesse de base de l'affichage du texte
    private float _speedMultiplier = 1f; //multiplicateur pour accélérer l'affichage du texte

    public int _characterPerCycle { get { return _speed <= 2f ? _characterMultiplier : _speed <= 2.5f ? _characterMultiplier * 2 : _characterMultiplier * 3; } }
    private int _characterMultiplier = 1; //Relatif au nb de caractères à afficher graduellement si l'affichage du texte doit être accéléré

    private bool _hurryUp = false; //Observe si l'affichage du texte doit être accéléré
    public bool hurryUp
    {
        get { return _hurryUp; }
        set { _hurryUp = value; }
    }
    private Coroutine _buildProcess = null;
    private bool _isBuilding => _buildProcess != null; //Observe si un processus d'affichage de texte est en cours
    public bool isBuilding => _isBuilding;

    public TextArchitect(TextMeshProUGUI tmpro_ui) //Assignation de variables lorsque le script est utilisé comme variable
    {
        _tmpro_ui = tmpro_ui;
    }
    public TextArchitect(TextMeshPro tmpro_world) //Assignation de variables lorsque le script est utilisé comme variable
    {
        _tmpro_world = tmpro_world;
    }

    /// <summary>
    /// Début de la construction du texte à afficher
    /// </summary>
    /// <param name="text">Texte à afficher</param>
    /// <returns>La coroutine du processus de construction du texte</returns>
    public Coroutine Build(string text)
    {
        _preText = "";
        _targetText = text;

        Stop();

        _buildProcess = tmpro.StartCoroutine(Building());
        return _buildProcess;
    }

    /// <summary>
    /// Joint du texte additionel sur le texte déjà affiché sur la scène. S'utilise lorsqu'un personnage renchéri sur la même réplique.
    /// </summary>
    /// <param name="text">Texte à afficher</param>
    /// <returns>La coroutine du processus de construction du texte</returns>
    public Coroutine Append(string text)
    {
        _preText = tmpro.text;
        _targetText = text;

        Stop();

        _buildProcess = tmpro.StartCoroutine(Building());
        return _buildProcess;
    }

    /// <summary>
    /// Arrête la construction de texte
    /// </summary>
    public void Stop()
    {
        if (!_isBuilding) return; //S'il n'y a pas de construction en cours, annule les actions de la fonction

        tmpro.StopCoroutine(_buildProcess);
        _buildProcess = null;
    }

    IEnumerator Building()
    {
        Prepare();

        switch (_buildMethod)
        {
            case BuildMethod.typewriter:
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Build_Fade();
                break;
        }
        OnComplete();
    }

    /// <summary>
    /// Appelé lorsque le processus de construction du texte à afficher est complété
    /// </summary>
    private void OnComplete()
    {
        _buildProcess = null;
        _hurryUp = false;
    }

    public void ForceComplete()
    {
        switch (_buildMethod)
        {
            case BuildMethod.typewriter:
                tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                break;
            case BuildMethod.fade:
                tmpro.ForceMeshUpdate();
                break;
        }

        Stop();
        OnComplete();
    }

    private void Prepare()
    {
        switch (_buildMethod)
        {
            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.typewriter:
                Prepare_Typewriter();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;
        }
    }

    /// <summary>
    /// Fonction qui affiche directement le texte voulu sans délai et transitions
    /// </summary>
    private void Prepare_Instant()
    {
        tmpro.color = tmpro.color;
        tmpro.text = _fullTargetText;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    }
    private void Prepare_Typewriter()
    {
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = _preText;

        if (_preText != "")
        {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        }

        tmpro.text += _targetText;
        tmpro.ForceMeshUpdate();
    }
    private void Prepare_Fade()
    {
        tmpro.text = _preText;
        if (_preText != "")
        {
            tmpro.ForceMeshUpdate();
            _preTextLength = tmpro.textInfo.characterCount;
        }
        else _preTextLength = 0;

        tmpro.text += _targetText;
        tmpro.maxVisibleCharacters = int.MaxValue;
        tmpro.ForceMeshUpdate();

        TMP_TextInfo textInfo = tmpro.textInfo;

        Color colorVisible = new Color(_textColor.r, _textColor.g, _textColor.b, 1);
        Color colorHidden = new Color(_textColor.r, _textColor.g, _textColor.b, 0);

        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible) continue;

            if (i < _preTextLength)
            {
                for (int v = 0; v < 4; v++)
                {
                    vertexColors[charInfo.vertexIndex + v] = colorVisible;
                }
            }
            else
            {
                for (int v = 0; v < 4; v++)
                {
                    vertexColors[charInfo.vertexIndex + v] = colorHidden;
                }
            }
        }

        tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    private IEnumerator Build_Typewriter()
    {
        while (tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            tmpro.maxVisibleCharacters += _hurryUp ? _characterPerCycle * 5 : _characterPerCycle;

            yield return new WaitForSeconds(0.015f / _speed);
        }
    }
    private IEnumerator Build_Fade()
    {
        int minRange = _preTextLength;
        int maxRange = minRange + 1;

        byte alphaTreshold = 15;

        TMP_TextInfo textInfo = tmpro.textInfo;

        Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;
        float[] alphas = new float[textInfo.characterCount];

        while (true)
        {
            float fadeSpeed = ((_hurryUp ? _characterPerCycle * 5 : _characterPerCycle) * _speed) * 4f;

            for (int i = minRange; i < maxRange; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible) continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                alphas[i] = Mathf.MoveTowards(alphas[i], 255, fadeSpeed);

                for (int v = 0; v < 4; v++)
                {
                    vertexColors[charInfo.vertexIndex + v].a = (byte)alphas[i];
                }

                if (alphas[i] >= 255) minRange++;
            }

            tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            bool lastCharacterIsInvisible = !textInfo.characterInfo[maxRange - 1].isVisible;

            if (alphas[maxRange - 1] > alphaTreshold || lastCharacterIsInvisible)
            {
                if (maxRange < textInfo.characterCount) maxRange++;
                else if (alphas[maxRange - 1] >= 255 || lastCharacterIsInvisible) break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
