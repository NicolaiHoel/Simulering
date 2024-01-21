# Simulering

I dette prosjektet ser jeg på et snøsmelting scenario. Jeg begrenset meg til å bare gjennomføre scenario 1 fordi det tok 4-5 timer.
Denne koden leser værdata (tid, nedbør og temperatur) inn fra en CSV-fil og skriver ut en CSV-fil med tilsvarende tidsoppløsning som inneholder snø, is, vann og tilsig som kommer fra fjellet

Antagelser i dette prosjektet:
Antar det er ingen snø i fjellet fra før.
Måler alt i nedbørsenheten mm.
Antar at alt kan måles i milimeter nedbør, velger derfor ikke å konvertere mellom enheter.

Denne koden har ikke gått gjennom en PeerReview og anbefales ikke å bli tatt i bruk i et produksjonsmiljø
Noen av de bugsene jeg løste (etter å ha lagd dem):

Bug 1:
IceAmount ble negativ fordi jeg hadde lagt til IceMelt to ganger i funksjonen CalculateMelt.
- Denne bugen ble løst ved å bruke Conditional Breakpoint med IceAmount < 0 i CalculateWaterPotential

Bug 2: 
WaterAmount ble større enn 60% av SnowAmount. Dette var fordi jeg glemte at når snø smelter og det blir mindre snø må det sjekkes for om det er mer enn 60% vann i snøen.
- Denne bugen ble løst ved å bruke Conditional Breakpoint med WaterAmount/SnowAmount > 0.6 i CalculateWaterPotential.

Noe som tok frustrerende mye tid:
- Å få CSV-innlesning til å funke.

Snuoprasjon underveis
- Gikk fra å representere vann og is som en prosentandel av snø til å lagre de som egene mengder. Det ble unødvendig komplekst med prosentandel.

Hva hadde jeg gjort med mer tid:
- Presentert data på en finere måte.
- Lagd flere tester
- Kvalitetsikret mer
- Gjort Scenario 2
