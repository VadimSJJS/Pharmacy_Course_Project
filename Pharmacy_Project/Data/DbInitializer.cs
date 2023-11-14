﻿using Pharmacy_Project.Data;
using Pharmacy_Project.Models;
using System.Data.SqlTypes;

namespace Pharmacy_Project.Data
{
    public class DbInitializer
    {
        private static Random random = new Random();
        private static List<string> Names = new()
        {
            "Аспирин",
            "Парацетамол",
            "Ибупрофен",
            "Амоксициллин",
            "Валериана",
            "Цетиризин",
            "Лоратадин",
            "Дексаметазон",
            "Ранитидин",
            "Метформин",
            "Ацетилцистеин",
            "Фенилэфрин",
            "Анальгин",
            "Диазепам",
            "Левофлоксацин",
            "Кларитромицин",
            "Атенолол",
            "Мидазолам",
            "Рамиприл",
            "Сертралин"
        };
        private static List<string> ShortDecsriptions = new()
        {
            "Противовоспалительное, жаропонижающее и болеутоляющее средство, используется при болях и воспалениях, а также для профилактики сердечно-сосудистых заболеваний.",
            "Анальгетик и жаропонижающее средство, эффективно при головной боли и лихорадке, без противовоспалительного эффекта.",
            "Нестероидное противовоспалительное средство (НПВС), облегчает боль и воспаление при различных заболеваниях.",
            "Антибиотик из группы пенициллинов, применяется для лечения бактериальных инфекций, в том числе респираторных и мочевыводящих путей.",
            "Травяное средство, используется для снятия нервного напряжения, бессонницы и улучшения сна.",
            "Антигистаминное средство, предназначено для лечения аллергических реакций, уменьшает зуд и отек.",
            "Антигистаминное средство, снижает проявления аллергии, включая крапивницу и насморк.",
            "Глюкокортикостероид, противовоспалительное и иммунодепрессивное средство, применяется при различных воспалительных и аллергических состояниях.",
            "Блокатор H2-гистаминовых рецепторов, используется для уменьшения выработки желудочного сока при язвенных заболеваниях и изжоге.",
            "Антидиабетическое средство, улучшает чувствительность тканей к инсулину, применяется при сахарном диабете типа 2.",
            "Противомукозное средство, помогает разжижать мокроту и облегчает ее отхождение при заболеваниях дыхательных путей.",
            "Сосудосуживающее средство, используется для снятия отечности слизистой оболочки носа при насморке.",
            "Противовоспалительное и жаропонижающее средство, применяется для лечения болей различного происхождения.",
            "Препарат группы бензодиазепинов, обладает анксиолитическим, седативным и противосудорожным действием.",
            "Антибиотик-фторхинолон, эффективен при лечении инфекций дыхательных путей и мочевыводящих путей.",
            "Антибиотик макролидной группы, применяется для лечения инфекций верхних и нижних дыхательных путей.",
            "Бета-блокатор, используется для снижения артериального давления и профилактики сердечных заболеваний.",
            "Краткодействующий седативный препарат, применяется в анестезиологии и для седации.",
            "Ингибитор ангиотензинпревращающего фермента (ИАПФ), применяется для лечения гипертонии и сердечной недостаточности.",
            "Антидепрессант из группы селективных ингибиторов обратного захвата серотонина, используется при лечении депрессии и тревожных расстройств.",
        };
        private static List<string> ActiveSubstance = new()
        {
            "Ацетилсалициловая кислота",
            "Парацетамол",
            "Ибупрофен",
            "Амоксициллин",
            "Экстракт корня валерианы",
            "Цетиризин",
            "Лоратадин",
            "Дексаметазон",
            "Ранитидин",
            "Метформин",
            "Ацетилцистеин",
            "Фенилэфрин",
            "Анальгин",
            "Диазепам",
            "Левофлоксацин",
            "Кларитромицин",
            "Атенолол",
            "Мидазолам",
            "Рамиприл",
            "Сертралин"
        };
        private static List<string> StorageLocations = new()
        {
            "Хранить в прохладном, сухом месте при температуре до 25°C, в защищенном от света месте.",
            "Хранить при температуре до 25°C в сухом месте, вдали от прямых солнечных лучей.",
            "Хранить в прохладном и сухом месте, защищенном от света, при температуре не выше 25°C..",
            "Хранить при температуре до 25°C в сухом месте, в оригинальной упаковке..",
            "Хранить в прохладном, сухом месте, защищенном от света, при температуре до 25°C..",
            "Хранить при температуре до 25°C в сухом и защищенном от света месте.",
            "ЛоратадинC.",
            "Хранить в прохладном, сухом месте при температуре до 25°C.",
            "Хранить при температуре до 25°C в защищенном от света месте..",
            "Хранить при температуре до 25°C в сухом месте.",
            "Хранить при температуре до 25°C в сухом месте.",
            "Хранить при температуре до 25°C в сухом и защищенном от света месте.",
            "Хранить при температуре до 25°C в сухом месте, в оригинальной упаковке.",
            "Хранить при температуре до 25°C в сухом месте.",
            "Хранить при температуре до 25°C в сухом месте.",
            "Хранить при температуре до 25°C в сухом месте, вдали от прямых солнечных лучей.",
            "Хранить в прохладном, сухом месте при температуре до 25°C, в защищенном от света месте.",
            "Хранить в прохладном, сухом месте, защищенном от света, при температуре до 25°C.",
            "Хранить в прохладном, сухом месте при температуре до 25°C.",
            "Хранить при температуре до 25°C в сухом месте, в оригинальной упаковке."
        };
        private static List<string> NameProducers = new()
        {
            "Байер, Новартис, Пфайзер.",
            "Джонсон и Джонсон, ГлаксоСмитКлайн, Новартис.",
            "Пфайзер, Джонсон и Джонсон, Рекитт Бенкайзер.",
            "ГлаксоСмитКлайн, Пфайзер, Тева.",
            "Нэйчерс Уэй, Нау Фудс, Хербал Гло.",
            "Джонсон и Джонсон (Зиртек), УСБ Фарма (Ксизал), Сан Фарма.",
            "Байер (Кларитин), Джонсон и Джонсон (Алаверт), Перриго.",
            "Мерк, Пфайзер, Тева.",
            "ГлаксоСмитКлайн, Пфайзер, Бёрингер Ингельхайм.",
            "Мерк, Тева, Майлан.",
            "Замбон, Эгис Фармасьютикалс, Фармазак.",
            "Байер, Новартис, Пфайзер.",
            "Санофи, Байер, Гедеон Рихтер.",
            "Рош, Майлан, Тева.",
            "Джонсон и Джонсон, Новартис, Санофи.",
            "Эбботт Лэбораториз, Пфайзер, Сандоз.",
            "АстраЗенека, Тенок, Сандоз.",
            "Рош, Фрезениус Каби, Бакстер Интернэшнл.",
            "Санофи, Тева, АстраЗенека.",
            "Пфайзер, Гринстоун, Тева.",
        };
        private static List<string> Countries = new()
        {
            "Германия (Байер), Швейцария (Новартис), США (Пфайзер).",
            "США (Джонсон и Джонсон), Великобритания (ГлаксоСмитКлайн), Швейцария (Новартис).",
            "США (Пфайзер), США (Джонсон и Джонсон), Великобритания (Рекитт Бенкайзер).",
            "Великобритания (ГлаксоСмитКлайн), США (Пфайзер), Израиль (Тева).",
            "Канада (Nature's Way), США (Now Foods), Канада (Herbal Glo).",
            "США (Джонсон и Джонсон), Бельгия (UCB Pharma), Индия (Sun Pharma).",
            "Германия (Байер), США (Джонсон и Джонсон), США (Перриго).",
            "Германия (Мерк), США (Пфайзер), Израиль (Тева).",
            "Великобритания (ГлаксоСмитКлайн), США (Пфайзер), Германия (Бёрингер Ингельхайм).",
            "Германия (Мерк), Израиль (Тева), Индия (Mylan).",
            "Италия (Zambon), Венгрия (Egis Pharmaceuticals), Россия (Pharmazac).",
            "Германия (Байер), Швейцария (Новартис), США (Пфайзер).",
            "Франция (Sanofi), Германия (Байер), Венгрия (Gedeon Richter).",
            "Швейцария (Roche), Индия (Mylan), Израиль (Teva).",
            "США (Джонсон и Джонсон), Швейцария (Новартис), Франция (Sanofi).",
            "США (Abbott Laboratories), США (Pfizer), Германия (Sandoz).",
            "Великобритания (AstraZeneca), Индия (Tenoch), Германия (Sandoz).",
            "Швейцария (Roche), Германия (Fresenius Kabi), США (Baxter International).",
            "Франция (Sanofi), Израиль (Teva), Великобритания (AstraZeneca).",
            "США (Pfizer), США (Greenstone), Израиль (Teva).",
        };

        private static List<int> InternationalCodes = new()
        {
            117527,
            481022,
            673042,
            246512,
            851057,
            174910,
            929123,
            374124,
            164812,
            397412,
            673920,
            750285,
            137152,
            846504,
            759205,
            917522,
            256400,
            500101,
            120220,
            141411
        };

        private static List<string> UnitMeasurements = new()
        {
            "оспообразующая единица (бляшкообразующее число)",
            "микрограмм",
            "титриционная единица",
            "процент",
            "единицы дозы полупатолоанатомических изменений",
            "резенфолд",
            "процент массовый",
            "индекс реактивности (биологическая единица стандартизации) на миллилитр",
            "грамм на миллилитр",
        };

        private static List<string> Providers = new()
        {
            "ПрофитМед",
            "НПО Гарант",
            "PHARMA Group",
            "МедТорг",
            "ООО МФК Биоритм",
        };

        private static List<string> NameDiseases = new()
        {
            "Неврофлюкс",
            "Экзоплазмоз",
            "Кристаллитарная астения",
            "Гиперквантовая дисгармония",
            "Лимфоцибернетический дисбаланс",
            "Синдром магнитной резонансной аномалии",
            "Гиперполимеразная дистрофия",
            "Космическая аллергия",
            "Оксидативная диссонансия",
            "Электромагнитный паралич",
            "Пикофобия",
            "Биоцифровая аномалия",
            "Квантовый астматизм",
            "Микроракетарная гиперактивность",
            "Эпигенетическая амнезия",
            "Нанобактериальная тахикардия",
            "Психосоматическая тераберта",
            "Вирусная хромота",
            "Радиоактивный синдром",
            "Интергалактическая нейродисгармония",
        };

        public static void Initialize(PharmacyContext db)
        {
            db.Database.EnsureCreated();

            // таблица Producer
            if (!db.Producers.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    string NameProducer = NameProducers[random.Next(0, NameProducers.Count)];
                    string Country = Countries[random.Next(0, Countries.Count)];
                    db.Add(new Producer() { Name = NameProducer, Country = Country });
                }
                db.SaveChanges();
            }

            // таблица Disease
            if (!db.Diseases.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    int InternatinalCode = InternationalCodes[random.Next(0, InternationalCodes.Count)];
                    string Name = NameDiseases[random.Next(0, NameDiseases.Count)];
                    db.Add(new Disease() { InternationalCode = InternatinalCode, Name = Name });
                }
                db.SaveChanges();
            }

            // таблица Medicine
            if (!db.Medicines.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    string Name = Names[random.Next(0, Names.Count)];
                    string ShortDecsription = ShortDecsriptions[random.Next(0, ShortDecsriptions.Count)];
                    string ActiveSubstances = ActiveSubstance[random.Next(0, ActiveSubstance.Count)];
                    int ProducerId = random.Next(1, 21);
                    string UnitMeasurement = UnitMeasurements[random.Next(0, UnitMeasurements.Count)];
                    int Count = Math.Abs(random.Next()) % 20;
                    string StorageLocation = StorageLocations[random.Next(0, StorageLocations.Count)];
                    db.Add(new Medicine() { Name = Name, ShortDescription = ShortDecsription, ActiveSubstance = ActiveSubstances, ProducerId = ProducerId, UnitMeasurement = UnitMeasurement, Count = Count, StorageLocation = StorageLocation });
                }
                db.SaveChanges();
            }

            
            // таблица MedicinesForDiseases
            if (!db.MedicinesForDiseases.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    int MedicineId = random.Next(1, 21);
                    int DiseaseId = random.Next(1, 21);

                    db.Add(new MedicinesForDisease() { MidicineId = MedicineId, DiseaseId = DiseaseId });
                }
                db.SaveChanges();
            }
            
            // таблица Outgoing
            if (!db.Outgoings.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    int MedicineId = random.Next(1, 21);
                    DateTime ImplemetionDate = DateTime.Now.AddYears(-random.Next(0, 100));
                    int Count = Math.Abs(random.Next()) % 20;
                    decimal SellingPrice = (decimal)random.NextDouble() * 10000;
                    db.Add(new Outgoing() { MedicineId = MedicineId, ImplementationDate = ImplemetionDate, Count = Count, SellingPrice = SellingPrice });
                }
                db.SaveChanges();
            }

            // таблица Incoming
            if (!db.Incomings.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    int MedicineId = random.Next(1, 21);
                    DateTime ArrivalDate = DateTime.Now.AddYears(-random.Next(0, 100));
                    int Count = Math.Abs(random.Next()) % 20;
                    string Provider = Providers[random.Next(0, Providers.Count)];
                    decimal Price = (decimal)random.NextDouble() * 10000;
                    db.Add(new Incoming() { MedicineId = MedicineId, ArrivalDate = ArrivalDate, Count = Count, Provider = Provider, Price = Price });
                }
                db.SaveChanges();
            }
        }
    }
}
