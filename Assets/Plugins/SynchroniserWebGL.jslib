mergeInto(LibraryManager.library, {

    InitialiserSauvegarde: function ()
    {
        FS.mkdir('/idbfs');

        FS.mount(IDBFS, {}, '/idbfs');

        FS.syncfs(true, function (err)
        {
            if (err)
            {
                console.error("Erreur init IDBFS:", err);
            }
            else
            {
                console.log("IDBFS initialisé");
            }
        });
    },

    SynchroniserWebGL: function ()
    {
        FS.syncfs(false, function (err)
        {
            if (err)
            {
                console.error("Erreur sync:", err);
            }
            else
            {
                console.log("Sauvegarde synchronisée");
            }
        });
    }

});