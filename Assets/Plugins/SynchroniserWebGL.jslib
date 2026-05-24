mergeInto(LibraryManager.library, {
  SynchroniserWebGL: function () {
    FS.syncfs(false, function (err) {
      if (err) {
        console.error("WebGL sync error:", err);
      } else {
        console.log("WebGL filesystem synchronized");
      }
    });
  },
});
