// Translation Dictionary
const i18n = {
    en: {
        "nav-dashboard": "Dashboard",
        "nav-details": "Details",
        "nav-settings": "Settings"
    },
    pt: {
        "nav-dashboard": "Painel",
        "nav-details": "Detalhes",
        "nav-settings": "Ajustes"
    },
    es: {
        "nav-dashboard": "Tablero",
        "nav-details": "Detalles",
        "nav-settings": "Ajustes"
    }
};

// State Variables
let currentInput = "0";
let previousInput = "";
let operation = null;
let history = [];

// DOM References
const screen = document.getElementById("screen");
const preview = document.getElementById("preview");
const themeToggle = document.getElementById("theme-toggle");
const langSelect = document.getElementById("lang-select");

// Theme Control Logic
themeToggle.addEventListener("click", () => {
    const currentTheme = document.documentElement.getAttribute("data-theme");
    const newTheme = currentTheme === "dark" ? "light" : "dark";
    document.documentElement.setAttribute("data-theme", newTheme);
    
    // Switch toggle icon
    const icon = themeToggle.querySelector("i");
    if(newTheme === "light") {
        icon.className = "fas fa-sun";
    } else {
        icon.className = "fas fa-moon";
    }
});

// Internationalization Selector
langSelect.addEventListener("change", (e) => {
    const selectedLang = e.target.value;
    document.documentElement.setAttribute("data-lang", selectedLang);
    
    document.querySelectorAll("[data-i18n]").forEach(element => {
        const key = element.getAttribute("data-i18n");
        element.textContent = i18n[selectedLang][key];
    });
});

// UI View Update Logic
function updateScreen() {
    screen.textContent = currentInput;
    if (operation && previousInput) {
        preview.textContent = `${previousInput} ${getOpSymbol(operation)}`;
    } else {
        preview.textContent = "";
    }
}

function getOpSymbol(op) {
    if (op === "add") return "+";
    if (op === "subtract") return "-";
    if (op === "multiply") return "×";
    if (op === "divide") return "÷";
    return "";
}

// Engine Interaction Event Listeners
document.querySelectorAll(".num").forEach(button => {
    button.addEventListener("click", () => {
        const val = button.textContent;
        if (currentInput === "0" && val !== ".") {
            currentInput = val;
        } else {
            if (val === "." && currentInput.includes(".")) return;
            currentInput += val;
        }
        updateScreen();
    });
});

document.querySelectorAll(".op").forEach(button => {
    button.addEventListener("click", () => {
        operation = button.getAttribute("data-op");
        previousInput = currentInput;
        currentInput = "0";
        updateScreen();
    });
});

document.querySelector("[data-action='clear']").addEventListener("click", () => {
    currentInput = "0";
    previousInput = "";
    operation = null;
    updateScreen();
});

document.querySelector("[data-action='toggle-sign']").addEventListener("click", () => {
    currentInput = (parseFloat(currentInput) * -1).toString();
    updateScreen();
});

document.querySelector("[data-action='percent']").addEventListener("click", () => {
    currentInput = (parseFloat(currentInput) / 100).toString();
    updateScreen();
});

document.querySelector(".equals").addEventListener("click", () => {
    if (!operation || !previousInput) return;
    
    let result = 0;
    const prev = parseFloat(previousInput);
    const current = parseFloat(currentInput);

    switch (operation) {
        case "add": result = prev + current; break;
        case "subtract": result = prev - current; break;
        case "multiply": result = prev * current; break;
        case "divide": 
            result = current === 0 ? "Error" : prev / current; 
            break;
    }

    currentInput = result.toString();
    operation = null;
    previousInput = "";
    updateScreen();
});
