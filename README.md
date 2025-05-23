"# BOApp" 
System budowany jest oraz rozwijany w celu zaspokojenia wewnętrznych (BackOffice) potrzeb firmy. Automatyzujemy lub digitalizujemy procesy wewnętrzne.
Aplikacje w systemie zbudowane są w jednolitym szablonie
  o	Formularz kliencki
  o	Lista formularzy
  o	Modele w warstwie aplikacji
  o	CQRS
  o	Opcjonalnie wysyłka email
  o	Opcjonalnie Job’y (Quartz)

Elementy do wyceny:
-	Konstrukcja systemu
  o	Architektura
  o	Usługi bazodanowe
  o	Usługi integracji z Azure
  o	Obsługa danych o pracownikach (import i aktualizacja)

-	Aplikacje:
  o	Ogólne systemowe słowniki
    	Definicje organizacji (role użytkowników)
    	Słownik zastępstw
  o	Magazyn IT (rejestracja środków trwałych, ruchy środków trwałych itp.)
    	Główny formularz Asset
    	Formularze słownikowe 
  o	DeferralPayment - Księgowość Rozrachunki (przyjmuje requesty z zewnętrznej aplikacji i zapewnia workflow akceptacyjny)
    	Główny formularz 
  o	AccountingNote – Noty Księgowe 
    	Główny formularz
  o	BNP – wyszukiwarka wyciągów bankowych
  o	BusinessTrips – Delegacje
    	Główny formularz
  o	Zaliczki – Advance Payment
    	Główny formularz
