using Microsoft.EntityFrameworkCore.Migrations;

namespace TITS_API.Repositories.Migrations
{
    public partial class HazardStatements_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

INSERT INTO public.""HazardStatements"" VALUES (1, 'H200', 'Materiały wybuchowe niestabilne', 'Unstable Explosive');
INSERT INTO public.""HazardStatements"" VALUES (2, 'H201', 'Materiał wybuchowy; Zagrożenie wybuchem masowym', 'Explosive; mass explosion hazard');
INSERT INTO public.""HazardStatements"" VALUES (3, 'H202', 'Materiał wybuchowy, poważne zagrożenie rozrzutem', 'Explosive; severe projection hazard');
INSERT INTO public.""HazardStatements"" VALUES (4, 'H203', 'Materiał wybuchowy; zagrożenie pożarem, wybuchem lub rozrzutem', 'Explosive; fire, blast or projection hazard');
INSERT INTO public.""HazardStatements"" VALUES (5, 'H204', 'Zagrożenie pożarem lub rozrzutem', 'Fire or projection hazard');
INSERT INTO public.""HazardStatements"" VALUES (6, 'H205', 'Może wybuchać masowo w przypadku pożaru', 'May mass explode in fire');
INSERT INTO public.""HazardStatements"" VALUES (7, 'H206', 'Zagrożenie pożarem, wybuchem lub rozrzutem; zwiększone ryzyko eksplozji, jeśli zmniejszony zostanie czynnik odczulający', 'Fire, blast or projection hazard; increased risk of explosion if desensitizing agent is reduced');
INSERT INTO public.""HazardStatements"" VALUES (8, 'H207', 'Zagrożenie pożarem lub rozrzutem; zwiększone ryzyko eksplozji, jeśli zmniejszony zostanie czynnik odczulający', 'Fire or projection hazard; increased risk of explosion if desensitizing agent is reduced');
INSERT INTO public.""HazardStatements"" VALUES (9, 'H208', 'Zagrożenie pożarem; zwiększone ryzyko eksplozji, jeśli zmniejszony zostanie czynnik odczulający', 'Fire hazard; increased risk of explosion if desensitizing agent is reduced');
INSERT INTO public.""HazardStatements"" VALUES (10, 'H220', 'Skrajnie łatwopalny gaz', 'Extremely flammable gas');
INSERT INTO public.""HazardStatements"" VALUES (11, 'H221', 'Gaz łatwopalny', 'Flammable gas');
INSERT INTO public.""HazardStatements"" VALUES (12, 'H222', 'Skrajnie łatwopalny aerozol', 'Extremely flammable aerosol');
INSERT INTO public.""HazardStatements"" VALUES (13, 'H223', 'Łatwopalny aerozol', 'Flammable aerosol');
INSERT INTO public.""HazardStatements"" VALUES (14, 'H224', 'Skrajnie łatwopalna ciecz i pary', 'Extremely flammable liquid and vapor');
INSERT INTO public.""HazardStatements"" VALUES (15, 'H225', 'Wysoce łatwopalna ciecz i pary', 'Highly Flammable liquid and vapor');
INSERT INTO public.""HazardStatements"" VALUES (16, 'H226', 'Łatwopalna ciecz i pary', 'Flammable liquid and vapor');
INSERT INTO public.""HazardStatements"" VALUES (17, 'H227', 'Palna ciecz', 'Combustible liquid');
INSERT INTO public.""HazardStatements"" VALUES (18, 'H228', 'Substancja stała łatwopalna', 'Flammable solid');
INSERT INTO public.""HazardStatements"" VALUES (19, 'H229', 'Pojemnik pod ciśnieniem: Ogrzanie grozi wybuchem.', 'Pressurized container: may burst if heated');
INSERT INTO public.""HazardStatements"" VALUES (20, 'H230', 'Może reagować wybuchowo nawet bez dostępu powietrza', 'May react explosively even in the absence of air');
INSERT INTO public.""HazardStatements"" VALUES (21, 'H231', 'Może reagować wybuchowo nawet bez dostępu powietrza pod zwiększonym ciśnieniem i/lub po ogrzaniu', 'May react explosively even in the absence of air at elevated pressure and/or temperature');
INSERT INTO public.""HazardStatements"" VALUES (22, 'H232', 'Może zapalić się samorzutnie w przypadku wystawienia na działanie powietrza', 'May ignite spontaneously if exposed to air');
INSERT INTO public.""HazardStatements"" VALUES (23, 'H240', 'Ogrzanie grozi wybuchem', 'Heating may cause an explosion');
INSERT INTO public.""HazardStatements"" VALUES (24, 'H241', 'Ogrzanie może spowodować pożar lub wybuch', 'Heating may cause a fire or explosion');
INSERT INTO public.""HazardStatements"" VALUES (25, 'H242', 'Ogrzanie może spowodować pożar', 'Heating may cause a fire');
INSERT INTO public.""HazardStatements"" VALUES (26, 'H250', 'Zapala się samorzutnie w przypadku wystawienia na działanie powietrza', 'Catches fire spontaneously if exposed to air');
INSERT INTO public.""HazardStatements"" VALUES (27, 'H251', 'Substancja samonagrzewająca się: może się zapalić', 'Self-heating; may catch fire');
INSERT INTO public.""HazardStatements"" VALUES (28, 'H252', 'Substancja samonagrzewająca się w dużych ilościach; może się zapalić', 'Self-heating in large quantities; may catch fire');
INSERT INTO public.""HazardStatements"" VALUES (29, 'H260', 'W kontakcie z wodą uwalniają łatwopalne gazy, które mogą ulegać samozapaleniu', 'In contact with water releases flammable gases which may ignite spontaneously');
INSERT INTO public.""HazardStatements"" VALUES (30, 'H261', 'W kontakcie z wodą uwalnia łatwopalne gazy', 'In contact with water releases flammable gas');
INSERT INTO public.""HazardStatements"" VALUES (31, 'H270', 'Może spowodować lub intensyfikować pożar; utleniacz', 'May cause or intensify fire; oxidizer');
INSERT INTO public.""HazardStatements"" VALUES (32, 'H271', 'Może spowodować pożar lub wybuch; silny utleniacz', 'May cause fire or explosion; strong Oxidizer');
INSERT INTO public.""HazardStatements"" VALUES (33, 'H272', 'Może intensyfikować pożar; utleniacz', 'May intensify fire; oxidizer');
INSERT INTO public.""HazardStatements"" VALUES (34, 'H280', 'Zawiera gaz pod ciśnieniem; ogrzanie grozi wybuchem', 'Contains gas under pressure; may explode if heated');
INSERT INTO public.""HazardStatements"" VALUES (35, 'H281', 'Zawiera schłodzony gaz; może spowodować oparzenia kriogeniczne lub obrażenia', 'Contains refrigerated gas; may cause cryogenic burns or injury');
INSERT INTO public.""HazardStatements"" VALUES (36, 'H290', 'Może powodować korozję metali', 'May be corrosive to metals');
INSERT INTO public.""HazardStatements"" VALUES (37, 'H300', 'Połknięcie grozi śmiercią', 'Fatal if swallowed');
INSERT INTO public.""HazardStatements"" VALUES (38, 'H301', 'Działa toksycznie po połknięciu', 'Toxic if swallowed');
INSERT INTO public.""HazardStatements"" VALUES (39, 'H302', 'Szkodliwy w przypadku połknięcia', 'Harmful if swallowed');
INSERT INTO public.""HazardStatements"" VALUES (40, 'H303', 'Może być szkodliwy w przypadku połknięcia', 'May be harmful if swallowed');
INSERT INTO public.""HazardStatements"" VALUES (41, 'H304', 'Połknięcie i dostanie się przez drogi oddechowe może grozić śmiercią', 'May be fatal if swallowed and enters airways');
INSERT INTO public.""HazardStatements"" VALUES (42, 'H305', 'Połknięcie i dostanie się przez drogi oddechowe może grozić śmiercią', 'May be fatal if swallowed and enters airways');
INSERT INTO public.""HazardStatements"" VALUES (43, 'H310', 'Grozi śmiercią w kontakcie ze skórą', 'Fatal in contact with skin');
INSERT INTO public.""HazardStatements"" VALUES (44, 'H311', 'Działa toksycznie w kontakcie ze skórą', 'Toxic in contact with skin');
INSERT INTO public.""HazardStatements"" VALUES (45, 'H312', 'Działa szkodliwie w kontakcie ze skórą', 'Harmful in contact with skin');
INSERT INTO public.""HazardStatements"" VALUES (46, 'H313', 'Może działać szkodliwie w kontakcie ze skórą', 'May be harmful in contact with skin');
INSERT INTO public.""HazardStatements"" VALUES (47, 'H314', 'Powoduje poważne oparzenia skóry oraz uszkodzenia oczu', 'Causes severe skin burns and eye damage');
INSERT INTO public.""HazardStatements"" VALUES (48, 'H315', 'Działa drażniąco na skórę', 'Causes skin irritation');
INSERT INTO public.""HazardStatements"" VALUES (49, 'H316', 'Powoduje podrażnienie skóry', 'Causes mild skin irritation');
INSERT INTO public.""HazardStatements"" VALUES (50, 'H317', 'Może powodować reakcję alergiczną skóry', 'May cause an allergic skin reaction');
INSERT INTO public.""HazardStatements"" VALUES (51, 'H318', 'Powoduje poważne uszkodzenia oczu', 'Causes serious eye damage');
INSERT INTO public.""HazardStatements"" VALUES (52, 'H319', 'Działa drażniąco na oczy', 'Causes serious eye irritation');
INSERT INTO public.""HazardStatements"" VALUES (53, 'H320', 'Powoduje podrażnienie oczu', 'Causes eye irritation');
INSERT INTO public.""HazardStatements"" VALUES (54, 'H330', 'Wdychanie grozi śmiercią', 'Fatal if inhaled');
INSERT INTO public.""HazardStatements"" VALUES (55, 'H331', 'Działa toksycznie w następstwie wdychania', 'Toxic if inhaled');
INSERT INTO public.""HazardStatements"" VALUES (56, 'H332', 'Działa szkodliwie w następstwie wdychania', 'Harmful if inhaled');
INSERT INTO public.""HazardStatements"" VALUES (57, 'H333', 'Może być szkodliwe w przypadku wdychania', 'May be harmful if inhaled');
INSERT INTO public.""HazardStatements"" VALUES (58, 'H334', 'Może powodować objawy alergii lub astmy lub trudności w oddychaniu w następstwie wdychania', 'May cause allergy or asthma symptoms or breathing difficulties if inhaled');
INSERT INTO public.""HazardStatements"" VALUES (59, 'H335', 'Może powodować podrażnienie dróg oddechowych', 'May cause respiratory irritation');
INSERT INTO public.""HazardStatements"" VALUES (60, 'H336', 'Może powodować senność lub zawroty głowy', 'May cause drowsiness or dizziness');
INSERT INTO public.""HazardStatements"" VALUES (61, 'H340', 'Może powodować wady genetyczne', 'May cause genetic defects');
INSERT INTO public.""HazardStatements"" VALUES (62, 'H341', 'Podejrzewa się, że powoduje wady genetyczne', 'Suspected of causing genetic defects');
INSERT INTO public.""HazardStatements"" VALUES (63, 'H350', 'Może powodować raka', 'May cause cancer');
INSERT INTO public.""HazardStatements"" VALUES (64, 'H350i', 'Może powodować raka w następstwie narażenia drogą oddechową', 'May cause cancer by inhalation');
INSERT INTO public.""HazardStatements"" VALUES (65, 'H351', 'Podejrzewa się, że powoduje raka', 'Suspected of causing cancer');
INSERT INTO public.""HazardStatements"" VALUES (66, 'H360', 'Może działać szkodliwie na płodność lub na dziecko w łonie matki', 'May damage fertility or the unborn child');
INSERT INTO public.""HazardStatements"" VALUES (67, 'H360F', 'Może działać szkodliwie na płodność', 'May damage fertility');
INSERT INTO public.""HazardStatements"" VALUES (68, 'H360D', 'Może działać szkodliwie na dziecko w łonie matki', 'May damage the unborn child');
INSERT INTO public.""HazardStatements"" VALUES (69, 'H360FD', 'Może działać szkodliwie na płodność; Może działać szkodliwie na dziecko w łonie matki', 'May damage fertility; May damage the unborn child');
INSERT INTO public.""HazardStatements"" VALUES (70, 'H360Fd', 'Może działać szkodliwie na płodność; Podejrzewa się, że działa szkodliwie na dziecko w łonie matki', 'May damage fertility; Suspected of damaging the unborn child');
INSERT INTO public.""HazardStatements"" VALUES (71, 'H360Df', 'Może działać szkodliwie na dziecko w łonie matki; Podejrzewa się, że działa szkodliwie na płodność', 'May damage the unborn child; Suspected of damaging fertility');
INSERT INTO public.""HazardStatements"" VALUES (72, 'H361', 'Podejrzewa się, że działa szkodliwie na płodność lub na dziecko w łonie matki', 'Suspected of damaging fertility or the unborn child');
INSERT INTO public.""HazardStatements"" VALUES (73, 'H361f', 'Podejrzewa się, że uszkodzenie płodności', 'Suspected of damaging fertility');
INSERT INTO public.""HazardStatements"" VALUES (74, 'H361d', 'Podejrzewa się, że działa szkodliwie na dziecko w łonie matki', 'Suspected of damaging the unborn child');
INSERT INTO public.""HazardStatements"" VALUES (75, 'H361fd', 'Podejrzewa się, że uszkodzenie płodności; Podejrzewa się, że działa szkodliwie na dziecko w łonie matki', 'Suspected of damaging fertility; Suspected of damaging the unborn child');
INSERT INTO public.""HazardStatements"" VALUES (76, 'H362', 'Może działać szkodliwie na dzieci karmione piersią', 'May cause harm to breast-fed children');
INSERT INTO public.""HazardStatements"" VALUES (77, 'H370', 'Powoduje uszkodzenie narządów', 'Causes damage to organs');
INSERT INTO public.""HazardStatements"" VALUES (78, 'H371', 'Może powodować uszkodzenie narządów', 'May cause damage to organs');
INSERT INTO public.""HazardStatements"" VALUES (79, 'H372', 'Powoduje uszkodzenie narządów poprzez długotrwałe lub wielokrotne narażenie', 'Causes damage to organs through prolonged or repeated exposure');
INSERT INTO public.""HazardStatements"" VALUES (80, 'H373', 'Powoduje uszkodzenie narządów poprzez długotrwałe lub wielokrotne narażenie', 'Causes damage to organs through prolonged or repeated exposure');
INSERT INTO public.""HazardStatements"" VALUES (81, 'H400', 'Działa bardzo toksycznie na organizmy wodne,', 'Very toxic to aquatic life');
INSERT INTO public.""HazardStatements"" VALUES (82, 'H401', 'Działa toksycznie na organizmy wodne', 'Toxic to aquatic life');
INSERT INTO public.""HazardStatements"" VALUES (83, 'H402', 'Działa szkodliwie na organizmy wodne', 'Harmful to aquatic life');
INSERT INTO public.""HazardStatements"" VALUES (84, 'H410', 'Działa bardzo toksycznie na organizmy wodne, powodując długotrwałe skutki', 'Very toxic to aquatic life with long lasting effects');
INSERT INTO public.""HazardStatements"" VALUES (85, 'H411', 'Działa toksycznie na organizmy wodne, powodując długotrwałe skutki', 'Toxic to aquatic life with long lasting effects');
INSERT INTO public.""HazardStatements"" VALUES (86, 'H412', 'Działa szkodliwie na organizmy wodne, powodując długotrwałe skutki', 'Harmful to aquatic life with long lasting effects');
INSERT INTO public.""HazardStatements"" VALUES (87, 'H413', 'Może powodować długotrwałe szkodliwe skutki dla organizmów wodnych', 'May cause long lasting harmful effects to aquatic life');
INSERT INTO public.""HazardStatements"" VALUES (88, 'H420', 'Działa szkodliwie na zdrowie publiczne i środowisko poprzez niszczące oddziaływanie na ozon w górnej warstwie atmosfery', 'Harms public health and the environment by destroying ozone in the upper atmosphere');
INSERT INTO public.""HazardStatements"" VALUES (89, 'EUH001', 'Wybuchowy w stanie suchym', 'Explosive when dry');
INSERT INTO public.""HazardStatements"" VALUES (90, 'EUH006', 'Wybuchowy z lub bez kontaktu z powietrzem', 'Explosive with or without contact with air');
INSERT INTO public.""HazardStatements"" VALUES (91, 'EUH014', 'Reaguje gwałtownie z wodą', 'Reacts violently with water');
INSERT INTO public.""HazardStatements"" VALUES (92, 'EUH018', 'Podczas stosowania mogą powstawać łatwopalne lub wybuchowe mieszaniny par z powietrzem', 'In use may form flammable/explosive vapor-air mixture');
INSERT INTO public.""HazardStatements"" VALUES (93, 'EUH019', 'Może tworzyć wybuchowe nadtlenki', 'May form explosive peroxides');
INSERT INTO public.""HazardStatements"" VALUES (94, 'EUH029', 'W kontakcie z wodą uwalnia toksyczne gazy', 'Contact with water liberates toxic gas');
INSERT INTO public.""HazardStatements"" VALUES (95, 'EUH031', 'W kontakcie z kwasami uwalnia toksyczne gazy', 'Contact with acids liberates toxic gas');
INSERT INTO public.""HazardStatements"" VALUES (96, 'EUH032', 'W kontakcie z kwasami uwalnia bardzo toksyczne gazy', 'Contact with acids liberates very toxic gas');
INSERT INTO public.""HazardStatements"" VALUES (97, 'EUH044', 'Zagrożenie wybuchem po ogrzaniu w zamkniętym pojemniku', 'Risk of explosion if heated under confinement');
INSERT INTO public.""HazardStatements"" VALUES (98, 'EUH059', 'Niebezpieczne dla warstwy ozonowej', 'Hazardous to the ozone layer');
INSERT INTO public.""HazardStatements"" VALUES (99, 'EUH066', 'Częsty kontakt może spowodować wysuszenie i pękanie skóry', 'Repeated exposure may cause skin dryness or cracking');
INSERT INTO public.""HazardStatements"" VALUES (100, 'EUH070', 'Działa toksycznie w kontakcie z oczami', 'Toxic by eye contact');
INSERT INTO public.""HazardStatements"" VALUES (101, 'AUH001', 'Wybuchowy w stanie suchym', 'Explosive when dry');
INSERT INTO public.""HazardStatements"" VALUES (102, 'AUH006', 'Wybuchowy z lub bez kontaktu z powietrzem', 'Explosive with or without contact with air');
INSERT INTO public.""HazardStatements"" VALUES (103, 'AUH014', 'Reaguje gwałtownie z wodą', 'Reacts violently with water');
INSERT INTO public.""HazardStatements"" VALUES (104, 'AUH018', 'Podczas stosowania mogą powstawać łatwopalne lub wybuchowe mieszaniny par z powietrzem', 'In use, may form flammable/explosive vapour/air mixture');
INSERT INTO public.""HazardStatements"" VALUES (105, 'AUH019', 'Mogą tworzyć wybuchowe nadtlenki', 'May form explosive peroxides');
INSERT INTO public.""HazardStatements"" VALUES (106, 'AUH029', 'W kontakcie z wodą uwalnia toksyczne gazy', 'Contact with water liberates toxic gas');
INSERT INTO public.""HazardStatements"" VALUES (107, 'AUH031', 'Kontakt z kwasem wydziela toksyczne gazy', 'Contact with acid liberates toxic gas');
INSERT INTO public.""HazardStatements"" VALUES (108, 'AUH032', 'Kontakt z kwasem wydziela bardzo toksyczne gazy', 'Contact with acid liberates very toxic gas');
INSERT INTO public.""HazardStatements"" VALUES (109, 'AUH044', 'Zagrożenie wybuchem po ogrzaniu w zamkniętym pojemniku', 'Risk of explosion if heated under confinement');
INSERT INTO public.""HazardStatements"" VALUES (110, 'AUH066', 'Częsty kontakt może spowodować wysuszenie i pękanie skóry', 'Repeated exposure may cause skin dryness and cracking');

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM public.""HazardStatements""");
        }
    }
}
