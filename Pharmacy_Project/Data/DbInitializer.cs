using Pharmacy_Project.Data;
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
            "Противовоспалительное",
            "Анальгетик",
            "НПВС",
            "Антибиотик",
            "Травяное средство",
            "Антигистаминное средство",
            "Антигистаминное средство",
            "Глюкокортикостероид",
            "Блокатор",
            "Антидиабетическое средство",
            "Противомукозное средство",
            "Сосудосуживающее средство",
            "Жаропонижающее",
            "Бензодиазепин",
            "Антибиотик-фторхинолон",
            "Антибиотик",
            "Бета-блокатор",
            "Седативный препарат",
            "ИАПФ",
            "Антидепрессант",
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
            "Прохладное место",
            "Сухое место",
            "Защищенное место",
            "Склад",
            "Холодильник",
            "Защищенное место от света",
            "ЛоратадинC.",
            "Влажное место",
            "Оригинальная упаковка",
            "Первичная упаковка",
            "Вторичная упоковка",
            "Потребительская упаковка",
            "Герметическая упаковка",
            "Укопоренная упаковка",
            "Холодное место",
            "Коробка",
            "Специализированные хранилища",
            "Аптечный шкаф",
            "Лаборатория",
            "Лекарственный автомат"
        };
        private static List<string> NameProducers = new()
        {
            "Байер",
            "Джонсон и Джонсон",
            "Пфайзер",
            "ГлаксоСмитКлайн",
            "Нэйчерс Уэй",
            "Джонсон и Джонсон (Зиртек)",
            "Байер (Кларитин)",
            "Мерк",
            "ГлаксоСмитКлайн",
            "Мерк",
            "Замбон",
            "Пфайзер.",
            "Санофи",
            "Рош",
            "Санофи.",
            "Эбботт Лэбораториз",
            "АстраЗенека",
            "Фрезениус Каби",
            "АстраЗенека",
            "Гринстоун",
        };
        private static List<string> Countries = new()
        {
            "Германия (Байер)",
            "США (Джонсон и Джонсон)",
            "США (Пфайзер)",
            "Великобритания (ГлаксоСмитКлайн)",
            "Канада (Nature's Way)",
            "Бельгия (UCB Pharma)",
            "США (Перриго).",
            "Германия (Мерк)",
            "Германия (Бёрингер Ингельхайм).",
            "Израиль (Тева)",
            "Италия (Zambon)",
            "Швейцария (Новартис)",
            "Франция (Sanofi)",
            "Швейцария (Roche)",
            "Швейцария (Новартис)",
            "США (Abbott Laboratories)",
            "Германия (Sandoz).",
            "США (Baxter International).",
            "Франция (Teva)",
            "США (Greenstone)",
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
            "оспообразующая единица",
            "микрограмм",
            "титриционная единица",
            "процент",
            "единицы дозы",
            "резенфолд",
            "процент массовый",
            "индекс реактивности на миллилитр",
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
                for (int i = 0; i < 50; i++)
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
                for (int i = 0; i < 50; i++)
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
                for (int i = 0; i < 50; i++)
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
                for (int i = 0; i < 50; i++)
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
                for (int i = 0; i < 50; i++)
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
                for (int i = 0; i < 50; i++)
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
