function changeAppTheme(themeId) {
    if (themeId === '0') {
        document.body.setAttribute('data-bs-theme', 'dark');
    } else {
        document.body.setAttribute('data-bs-theme', 'light');
    }
}