(function () {
  try {
    const t = localStorage.getItem('theme');
    if (t === 'dark') {
      document.documentElement.setAttribute('data-theme', 'dark');
    }
  } catch (e) { /* noop */ }

  window.themeToggle = {
    set: function (theme) {
      try {
        if (theme === 'dark') {
          document.documentElement.setAttribute('data-theme', 'dark');
        } else {
          document.documentElement.removeAttribute('data-theme');
        }
      } catch (e) { /* noop */ }
    },
    clear: function () {
      try {
        document.documentElement.removeAttribute('data-theme');
      } catch (e) { /* noop */ }
    }
  };
})();