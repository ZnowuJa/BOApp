System budowany jest oraz rozwijany w celu zaspokojenia wewnętrznych (BackOffice) potrzeb firmy. Automatyzujemy lub digitalizujemy procesy wewnętrzne.  
Aplikacje w systemie zbudowane są w jednolitym szablonie:  
- Formularz kliencki  
- Lista formularzy  
- Modele w warstwie aplikacji  
- CQRS  
- Opcjonalnie wysyłka email  
- Opcjonalnie Job’y (Quartz)  

### Elementy do wyceny:

#### Konstrukcja systemu  
- Architektura  
- Usługi bazodanowe  
- Usługi integracji z Azure  
- Obsługa danych o pracownikach (import i aktualizacja)  
- Środowiska robocze i integracja z AZB  
  - DEV  
  - PROD  

#### Aplikacje:  
- Ogólne systemowe słowniki  
  - Definicje organizacji (role użytkowników)  
  - Słownik zastępstw  
- Magazyn IT (rejestracja środków trwałych, ruchy środków trwałych itp.)  
  - Główny formularz Asset  
  - Formularze słownikowe  
- DeferralPayment – Księgowość Rozrachunki (przyjmuje requesty z zewnętrznej aplikacji i zapewnia workflow akceptacyjny)  
  - Główny formularz  
- AccountingNote – Noty Księgowe  
  - Główny formularz  
- BNP – wyszukiwarka wyciągów bankowych  
- BusinessTrips – Delegacje  
  - Główny formularz  
- Zaliczki – Advance Payment  
  - Główny formularz  
