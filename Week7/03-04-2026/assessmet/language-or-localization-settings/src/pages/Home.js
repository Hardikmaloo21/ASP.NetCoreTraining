import { useLanguage } from "../context/LanguageContext";

function Home() {
  const { t } = useLanguage();

  return (
    <div className="container">
      <div className="card">
        <h1>{t.welcome}</h1>
        <h2>{t.home}</h2>
      </div>
    </div>
  );
}

export default Home;