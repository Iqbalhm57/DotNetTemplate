// Load Theme and Language on Page Load
document.addEventListener("DOMContentLoaded", function () {
    // Theme Setup
    const theme = localStorage.getItem("theme") || "light";
    setTheme(theme);

    const themeToggleButton = document.getElementById("themeToggle");
    if (themeToggleButton) {
        themeToggleButton.addEventListener("click", function () {
            const currentTheme = document.documentElement.getAttribute("data-bs-theme");
            const newTheme = currentTheme === "light" ? "dark" : "light";
            setTheme(newTheme);
            localStorage.setItem("theme", newTheme);
        });
    }

    // Language Setup
    const language = localStorage.getItem("language") || "en";
    setLanguage(language);

    const languageSelect = document.getElementById("languageSelect");
    if (languageSelect) {
        languageSelect.value = language;
        languageSelect.addEventListener("change", function () {
            const selectedLanguage = this.value;
            setLanguage(selectedLanguage);
            localStorage.setItem("language", selectedLanguage);
            location.reload(); // Reload page to apply language (future enhancement)
        });
    }
});

// Function to Set Theme
function setTheme(theme) {
    document.documentElement.setAttribute("data-bs-theme", theme);

    const themeToggleButton = document.getElementById("themeToggle");
    if (themeToggleButton) {
        themeToggleButton.textContent = theme === "light" ? "🌙" : "☀️";
    }
}

// Function to Set Language (for now only saving the choice)
function setLanguage(lang) {
    const htmlTag = document.querySelector("html");
    if (htmlTag) {
        htmlTag.setAttribute("lang", lang);
    }
}
