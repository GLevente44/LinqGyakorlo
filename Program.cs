using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;

namespace LinqGyakorlo
{
    class Program
    {
        static void Main(string[] args)
        {
            // A feladatok leírását a Feladatlap.md fájlban találod.
            // Minden feladathoz tartozik egy Feladat##() metódus itt lent.
            // Írd meg a LINQ lekérdezést a metódus törzsében, majd
            // vedd ki a kommentet a hívása elől, hogy lásd az eredményt.

            Feladat01();
            Console.WriteLine("----------------------------------");
            Feladat02();
            Console.WriteLine("----------------------------------");
            Feladat03();
            Console.WriteLine("----------------------------------");
            Feladat04();
            Console.WriteLine("----------------------------------");
            Feladat05();
            Console.WriteLine("----------------------------------");
            Feladat06();
            Console.WriteLine("----------------------------------");
            Feladat07();
            Console.WriteLine("----------------------------------");
            Feladat08();
            Console.WriteLine("----------------------------------");
            Feladat09();
            Console.WriteLine("----------------------------------");
            Feladat10();
            Console.WriteLine("----------------------------------");
            Feladat11();
            Console.WriteLine("----------------------------------");
            Feladat12();
            Console.WriteLine("----------------------------------");
            Feladat13();
            Console.WriteLine("----------------------------------");
            Feladat14();
            Console.WriteLine("----------------------------------");
            // Feladat15();
            // Feladat16();
            // Feladat17();
            // Feladat18();
            // Feladat19();
            // Feladat20();
            // Feladat21();
            // Feladat22();
            // Feladat23();
            // Feladat24();
            // Feladat25();
            // Feladat26();
            // Feladat27();
            // Feladat28();
            // Feladat29();
            // Feladat30();
            // Feladat31();
            // Feladat32();
            // Feladat33();
            // Feladat34();
            // Feladat35();
            // Feladat36();
            // Feladat37();
            // Feladat38();
            // Feladat39();
            // Feladat40();
        }

        // ---------- 1. Szűrés — Where ----------

        // 1. Hallgatók, akiknek 4.0 fölötti az átlaga.
        static void Feladat01()
        {
            var result = SampleData.Students.Where(atlag => atlag.GradeAverage > 4);
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }

        // 2. Budapesti hallgatók.
        static void Feladat02()
        {
            var result = SampleData.Students.Where(varos => varos.City == "Budapest");
            foreach (var item in result) {
                Console.WriteLine(item);
            }
        }

        // 3. Kurzusok, amelyek kreditértéke legalább 5.
        static void Feladat03()
        {
            var result = SampleData.Courses.Where(kredit => kredit.Credit >= 5);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        // 4. Hallgatók 20-23 év között (határokkal), akik nem budapestiek.
        static void Feladat04()
        {
            var result = SampleData.Students.Where(tanulo => tanulo.Age >= 20 && tanulo.Age < 23 && tanulo.City != "Budapest");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        // ---------- 2. Vetítés — Select, SelectMany ----------

        // 5. Csak a hallgatók nevei.
        static void Feladat05()
        {
            var result = SampleData.Students.Select(nevek => nevek.Name);
            foreach (var item in result) {
                Console.WriteLine(item);
            }
        }

        // 6. Anonim típusú lista: Name, GradeAverage.
        static void Feladat06()
        {
            var result = SampleData.Students.Select(nevek => nevek.Name + nevek.GradeAverage);
            foreach (var item in result) {
                Console.WriteLine(item);
            }
        }

        // 7. Kurzus neve + a kurzust tartó tanár neve (Select, Join nélkül).
        static void Feladat07()
        {
            var result = SampleData.Courses.Select(c => new {
                KurzusNev = c.Name,
                TanarNev = SampleData.Teachers.First(t => t.Id == c.TeacherId).Name
            });
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        // 8. SelectMany: beiratkozások lapos listája hallgató névvel.
        static void Feladat08()
        {
            var result = SampleData.Students.SelectMany(s => SampleData.Enrollments.Where(e => e.StudentId == s.Id), (s, e) => new { TanuloNev = s.Name, KurzusId = e.CourseId, Jegy = e.Grade });
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        // ---------- 3. Rendezés — OrderBy, ThenBy, Reverse ----------

        // 9. Hallgatók átlag szerint csökkenő sorrendben.
        static void Feladat09()
        {
            var result = SampleData.Students.OrderByDescending(s => s.GradeAverage);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        // 10. Hallgatók város szerint, majd név szerint növekvő sorrendben.
        static void Feladat10()
        {
            var result = SampleData.Students.OrderBy(s => s.City).ThenBy(s => s.Name);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        // 11. Kurzusok eredeti sorrendjének megfordítása (Reverse).
        static void Feladat11()
        {
            var result = SampleData.Courses.AsEnumerable().Reverse();
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

        }

        // ---------- 4. Csoportosítás — GroupBy ----------

        // 12. Hallgatók száma városonként.
        static void Feladat12()
        {
            var result = SampleData.Students.GroupBy(s => s.City).Select(g => new { g.Key, Darab = g.Count() });
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        // 13. Átlagos tanulmányi átlag városonként.
        static void Feladat13()
        {
            var result = SampleData.Students.GroupBy(s => s.City).Select(g => new { Város = g.Key, Atlag = g.Average(avg => avg.GradeAverage) });
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        // 14. Kurzusnevek kategóriánként.
        static void Feladat14()
        {
            var result = SampleData.Courses.GroupBy(c => c.Category).Select(g => new { KurzusKategoria = g.Key, KurzusNevek = g.Select(nev => nev.Name) });
            foreach (var item in result)
            {
                Console.WriteLine(item.KurzusKategoria);
                foreach (var item2 in result)
                {
                    Console.WriteLine(item2);
                }
            }
        }

        // ---------- 5. Összekapcsolás — Join, GroupJoin ----------

        // 15. Enrollments + Students Join: hallgató neve minden beiratkozáshoz.
        static void Feladat15()
        {
            // TODO
        }

        // 16. Háromtáblás Join: hallgató neve, kurzus neve, érdemjegy.
        static void Feladat16()
        {
            // TODO
        }

        // 17. GroupJoin: hallgatónként a beiratkozásai (azok is, akiknek nincs).
        static void Feladat17()
        {
            // TODO
        }

        // ---------- 6. Halmazműveletek — Distinct, Union, Intersect, Except, Concat, Zip ----------

        // 18. Hány különböző város van a hallgatók között (Distinct).
        static void Feladat18()
        {
            // TODO
        }

        // 19. Különböző kurzuskategóriák (Distinct).
        static void Feladat19()
        {
            // TODO
        }

        // 20. Union, Intersect, Except a "kiváló" (átlag >= 4.5) és "budapesti" hallgatók nevei között.
        static void Feladat20()
        {
            // TODO
        }

        // 21. Concat: Matematika + Informatika kurzusnevek.
        static void Feladat21()
        {
            // TODO
        }

        // 22. Zip: első 4 hallgató neve + első 4 kurzus neve párban.
        static void Feladat22()
        {
            // TODO
        }

        // ---------- 7. Aggregálás — Count, Sum, Average, Min, Max, Aggregate ----------

        // 23. Hallgatók száma összesen, illetve akiknek átlaga > 4.0 (Count).
        static void Feladat23()
        {
            // TODO
        }

        // 24. Az összes kurzus kredit-összege (Sum).
        static void Feladat24()
        {
            // TODO
        }

        // 25. Hallgatók átlagéletkora (Average).
        static void Feladat25()
        {
            // TODO
        }

        // 26. Legfiatalabb és legidősebb hallgató életkora (Min, Max).
        static void Feladat26()
        {
            // TODO
        }

        // 27. Aggregate: hallgatónevek vesszővel elválasztva egy stringbe.
        static void Feladat27()
        {
            // TODO
        }

        // ---------- 8. Elemkiválasztás — First, Last, Single, ElementAt ----------

        // 28. Első szegedi hallgató (First/FirstOrDefault).
        static void Feladat28()
        {
            // TODO
        }

        // 29. Az egyetlen "Lakatos Kata" nevű hallgató (Single/SingleOrDefault),
        //     majd egy olyan eset kipróbálása try-catch-csel, ahol több találat van.
        static void Feladat29()
        {
            // TODO
        }

        // 30. A 3. indexű (0-tól) hallgató (ElementAt).
        static void Feladat30()
        {
            // TODO
        }

        // ---------- 9. Particionálás — Skip, Take, SkipWhile, TakeWhile, Chunk ----------

        // 31. TOP 3 hallgató átlag szerint (Take).
        static void Feladat31()
        {
            // TODO
        }

        // 32. Az első 3 utáni hallgatók (Skip).
        static void Feladat32()
        {
            // TODO
        }

        // 33. Életkor szerint rendezve: TakeWhile (21 évnél fiatalabbak), majd SkipWhile (a többi).
        static void Feladat33()
        {
            // TODO
        }

        // 34. Hallgatók felbontása 4 fős csoportokra (Chunk).
        static void Feladat34()
        {
            // TODO
        }

        // ---------- 10. Egyéb — Any, All, Contains, ToDictionary, ToHashSet, DefaultIfEmpty ----------

        // 35. Van-e hallgató 2.5 alatti átlaggal (Any).
        static void Feladat35()
        {
            // TODO
        }

        // 36. Minden hallgató 18 évesnél idősebb-e (All).
        static void Feladat36()
        {
            // TODO
        }

        // 37. Szerepel-e "Pécs" a városok között (Contains).
        static void Feladat37()
        {
            // TODO
        }

        // 38. Dictionary<int, string> a hallgatók Id-je és neve alapján (ToDictionary).
        static void Feladat38()
        {
            // TODO
        }

        // 39. HashSet<string> a kurzuskategóriákból (ToHashSet).
        static void Feladat39()
        {
            // TODO
        }

        // 40. Nem létező kurzushoz tartozó beiratkozások, DefaultIfEmpty kezeléssel.
        static void Feladat40()
        {
            // TODO
        }
    }
}
