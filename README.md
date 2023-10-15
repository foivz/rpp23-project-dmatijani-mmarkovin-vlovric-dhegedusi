# MyLibra

## Projektni tim

Ime i prezime | E-mail adresa (FOI) | JMBAG | Github korisničko ime
------------  | ------------------- | ----- | ---------------------
David Matijanić | dmatijani21@student.foi.hr | 0016153844 | dmatijani
Viktor Lovrić | vlovric21@student.foi.hr | 0016154953 | vlovric21
Domagoj  | dhegedusi21@student.foi.hr | 0016153732 | dhegedusi21
Magdalena Markovinović  | mmarkovin21@student.foi.hr | 0016155896 | mmarkoovin21

## Opis domene

Aplikacija MyLibra digitalizira proces posuđivanja knjiga u knjižnicama i vođenja evidencije knjiga. Aplikacija je namijenjena za članove knjižnice te zaposlenike knjižnice. Zaposlenici knjižnice imat će evidenciju nad knjigama na način da je vidljiv broj knjiga na policama, popis svih knjiga, pregled posuđenih knjiga i članova knjižnice. Članovi knjižnice koriste aplikaciju kako bi vidjeli dostupne knjige, posuđivali ih, pisali recenzije i pregledavali već postojeće.

## Specifikacija projekta

Aplikacija će koristiti dislociranu bazu podataka koja sadrži podatke o knjigama, zaposlenicima, članovima knjižnice, posudbama i ostalo. Korištenje aplikacije moguće je kroz 3 uloge:
* Administrator - administratori su razvojni tim aplikacije (u sklopu ovog projekta naš tim) te korisnička podrška. Uloga administratora jedina može registrirati nove zaposlenike knjižnice na sljedeći način: kupnjom našeg softvera administratori dodaju knjižnicu i njene zaposlenike u bazu podataka. Ako postojeća knjižnica zaposli novog zaposlenika ili otpusti postojećeg, kontaktira administratore koji podešavaju stanje u bazi podataka.
* Zaposlenik knjižnice - uloga koja upravlja podacima o knjižnici tako da unosi informacije o knjigama i njihovu količinu, pregledava stanje posuđenih knjiga, posudbe i količinu. Također vide recenzije na knjige što pišu članovi knjižnice. Zaposlenici knjižnice jedini mogu registrirati nove članove tako što ti članovi moraju fizički doći u knjižnicu kako bi se izbjeglo lažno registriranje članova.
* Član knjižnice - uloga i korisnički podaci za svakog člana se dobivaju u knjižnici. Članovi mogu pregledavati sve knjige, posuđivati, rezervirati ukoliko knjiga nije na stanju, pisati recenzije, primati obavijesti i pisati zapisnik.

U aplikaciji, zaposlenici u sustavu vezani su uz određenu knjižnicu (što unosi administrator) te se sve promjene u knjižnici (nove knjige, stanje), novi članovi i njihove posudbe vežu uz tu knjižnicu (knjižnice su izolirane). Kada zaposlenik registrira novog korisnika, odnosno člana knjižnice, taj član je u sustavu automatski zabilježen pod knjižnicu zaposlenika koji ga registrira.

Oznaka | Naziv | Kratki opis | Odgovorni član tima
------ | ----- | ----------- | -------------------
F01 | Ažuriranje i upravljanje knjižnicama i njihovim zaposlenicima (administrator) | Uloga administratora moći će u komunikaciji s knjižnicom dodati ih u sustav te davati zaposlenicima knjižnice njihove korisničke podatke koje će tada oni koristiti kako bi se ulogirali kao uloga zaposlenika. Po promjeni stanja zaposlenika u knjižnici administratori imaju opciju ažuriranja podataka. | David Matijanić
F02 | Prijava u sustav | Za pristup funkcionalnostima aplikacije korisnik se prethodno mora prijaviti. Aplikacija će nuditi drugačiju vrstu pristupa ovisno o ulozi korisnika. Uloge koje se razlikuju su: administrator, zaposlenik knjižnice i član knjižnice. | Magdalena Markovinović
F03 | Upravljanje knjigama i stanjem u knjižnici (zaposlenik) | Zaposlenici knjižnice mogu unositi nove knjige i količinu. Pri unosu knjige unosi se naslov, autor, opis, godina izdavanja i ostale informacije. | Viktor Lovrić
F04 | Upravljanje posudbama (zaposlenik) | Kada član knjižnice odluči posuditi neku knjigu (F09), zaposlenik u sustavu vidi da je ta knjiga zabilježena i da će biti posuđena. Kada član fizički dođe po knjigu, zaposlenik potvrđuje da je knjiga posuđena te se određuje trajanje posudbe (datum isteka se automatski izračuna). Zaposlenik ima pregled svih posuđenih knjiga te informacije o posudbi (datum posudbe, član i eventualno kašnjenje). Kada član vrati knjigu, zaposlenik za tu posudbu bilježi da je knjiga vraćena i knjiga se vraća na stanje. Ukoliko je član kasnio sa posudbom, pisat će broj dana koji kasni. | David Matijanić
F05 | Registracija i brisanje članova (zaposlenik) | Ukoliko se osoba fizički pojavi u knjižnici i zatraži članstvo, zaposlenik tu osobu upisuje te registrira u sustav nakon čega osoba postaje član knjižnice i dobiva svoj profil s podacima koje je zaposlenik prethodno definirao. Ukoliko neki član odluči prestati biti član knjižnice, knjižnica ga može ukloniti iz sustava. | Magdalena Markovinović
F06 | Pisanje i čitanje obavijesti | Na početnoj stranici aplikacije će se nalaziti panel s obavijestima (News feed) gdje će zaposlenici moći obavještavati korisnike o nabavi novih knjiga, određenim izvanrednim vijestima i slično. | Magdalena Markovinović
F07 | Pretraživanje i filtriranje knjiga (član) | Kako bi se olakšala navigacija u aplikaciji, korisnici će moći pretraživati knjige prema imenu te filtrirati knjige prema žanru, piscu, godini, dostupnosti i ostalim svojstvima. | Viktor Lovrić
F08 | Rezervacija knjige (član) | Ukoliko neka knjiga trenutno nije dostupna (nema je na zalihi), član knjižnice će moći rezervirati mjesto čekanja za tu knjigu. Kada se knjiga pojavi na zalihi, sustav obavještava zaposlenika knjižnice te prvog člana koji je knjigu stavio na listi čekanja kako bi ju mogao posuditi (unutar aplikacije te uživo pokupiti, F04 i F09). | Viktor Lovrić
F09 | Upravljanje posudbama (član) | Ukoliko član želi posuditi neku knjigu, pritišće opciju „posudi“ te ju mora fizički preuzeti u knjižnici (F04). Fizičkim preuzimanjem započinje brojanje dana od posudbe te član može vidjeti sve svoje posudbe i rok. Kada član fizički vrati knjigu, posudba se uklanja iz njegovih posudbi. | David Matijanić
F10 | Pregled knjiga i informacija (član) | Pri odabiru svake knjige, na zaslonu korisnika će se ispisati informacije o toj knjizi (ime knjige, autor, vrijeme izdavanja, kratki sadržaj i ostalo). | Domagoj Hegedušić
F11 | Pisanje i pregled recenzija (član, zaposlenik) | Svaki član knjižnice će nakon vraćanja knjige moći ostaviti recenziju na tu knjigu. Pri fizičkom vraćanju knjige, zaposlenik to unosi u sustav i tom članu će biti moguće ostavljanje recenzije. Recenzija se sastoji od ocjene (1-5) i opcionalnog komentara. Zaposlenici i ostali članovi knjižnice vide sve recenzije na odabrane knjige. | Domagoj Hegedušić
F12 | Zapisnik čitanja (član) | Pri odabiru svake knjige, korisnik će imati opciju zapisivanja svojih misli pri čitanju, komentara i ostalih napomena u zapisnik čitanja. Zapisnik čitanja je drugačiji za svaku knjigu. | Domagoj Hegedušić
F13 | Odjava | Svaki korisnik ima opciju odjavljivanja iz aplikacije. | Magdalena Markovinović

## Tehnologije i oprema

Za razvoj ove aplikacije koristit ćemo .Net Framework razvojni okvir. Razvojno okruženje je Visual Studio. Vrsta projekta je WPF jer je planirana samo za Windows kao desktop aplikacija. Baza podataka je dislocirana i nalazi se na serveru. Za verzioniranje programskog koda bit će korišten git i GitHub. Dokumentacija će biti napisana u GitHub Wiki, a projektni zadaci planirani u GitHub projects.
