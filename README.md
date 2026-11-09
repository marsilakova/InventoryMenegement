Lagerhanteringssystem (Inventory Management System)

Detta är ett individuellt mjukvaruprojekt i form av en konsolapplikation utvecklad i C# och .NET. Programmet är utformat för att hjälpa företag att hantera och kontrollera sitt produktlager på ett smidigt och effektivt sätt.

Systemets arkitektur bygger på principerna för objektorienterad programmering (OOP) och har en flexibel struktur som även ligger till grund för vidareutveckling, bland annat veckoprojektet Asset Tracking Level 2 (Vecka 45) .

🚀 Huvudfunktioner (CRUD)

Applikationen stöder fullständig datahantering via en interaktiv konsolmeny med följande 8 alternativ:
1. Add product (Create): Lägga till nya produkter med unika ID, pris, antal och kategori.
2. View all products (Read): Visa hela lagret i ett snyggt formaterat tabellformat med valutasymboler.
3. Update product (Update): Söka efter en specifik produkt via dess ID och uppdatera pris eller antal.
4. Delete product (Delete): Radera en artikel permanent från lagret.
5. Show total value: Beräkna och visa det totala ekonomiska värdet av hela lagret i realtid.
6. Sort by category: Sortera produktlistan alfabetiskt baserat på kategori.
7. Save data: Spara alla aktuella lagerdata permanent till en textfil.
8. Exit: Avsluta programmet på ett säkert sätt.

💾 Datalagring & Robusthet

• Automatisk filhantering: Systemet sparar data till en extern textfil (inventory.txt).
• Persistens: Vid programstart anropas metoden LoadData(), som automatiskt läser in sparade produkter rad för rad, så att ingen information går förlorad mellan körningarna.
• Indatavalidering: Programmet använder CultureInfo.InvariantCulture för att hantera decimaltecken (punkt) korrekt oavsett operativsystemets regionala inställningar, vilket förhindrar oväntade programkrascher.

🛠️ Arkitektur & Tekniker

• Programmeringsspråk: C# (.NET)
• Kollektioner: Generisk List&lt;Product> för dynamisk hantering av data i internminnet.
• Datamodell: Klassen Product med inkapslade egenskaper (Properties).
• Kontrollstrukturer:while-loop för programflödet och en switch-case-sats för menyvalen.

📈 Framtida utveckling

• Migrera datalagringen från en enkel textfil till en relationell SQL-databas med hjälp av Entity Framework.
• Vidareutveckla systemet till ett mer omfattande verktyg för Asset Tracking för att spåra avancerade företagstillgångar (hårdvara, mjukvarulicenser, etc.).