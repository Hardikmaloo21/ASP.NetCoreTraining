import { createContext, useContext, useState } from "react";

const LanguageContext = createContext();

const translations = {
  en: {
    welcome: "Welcome",
    home: "Home Page",
    about: "About Page",
    changeLang: "Change Language",
  },
  fr: {
    welcome: "Bienvenue",
    home: "Page d'accueil",
    about: "À propos",
    changeLang: "Changer de langue",
  },
  es: {
    welcome: "Bienvenido",
    home: "Página principal",
    about: "Acerca de",
    changeLang: "Cambiar idioma",
  },
  hi: {
    welcome: "स्वागत है",
    home: "होम पेज",
    about: "परिचय पेज",
    changeLang: "भाषा बदलें",
  },
};

export const LanguageProvider = ({ children }) => {
  const [language, setLanguage] = useState(
    localStorage.getItem("lang") || "en"
  );

  const changeLanguage = (lang) => {
    setLanguage(lang);
    localStorage.setItem("lang", lang);
  };

  return (
    <LanguageContext.Provider
      value={{
        language,
        setLanguage: changeLanguage,
        t: translations[language],
      }}
    >
      {children}
    </LanguageContext.Provider>
  );
};

export const useLanguage = () => useContext(LanguageContext);