import { useLanguage } from "../context/LanguageContext";

function About() {
  const { t } = useLanguage();

  return (
    <div className="container">
      <div className="card">
        <h1>{t.about}</h1>
      </div>
    </div>
  );
}

export default About;