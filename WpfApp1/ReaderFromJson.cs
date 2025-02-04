﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace WpfApp1
{
  
    internal class ReaderFromJson
    {
        public readonly string path = "JSON\\";
   
        public static List<User> people;
        public static List<User> ListForGridView; //Лист для отображения графика на другой форме в методе ...
        public List<UserInTable> ReadFromJsonFile()
        {
            List<User> BigUser = new List<User>();
            List<User> LocalUser = new List<User>();
            string[] files = Directory.GetFiles(path);
            foreach (var document in files)
            {
                using (StreamReader file = File.OpenText(document))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    people = (List<User>)serializer.Deserialize(file, typeof(List<User>));
                    foreach (var ass in people)
                    {
                        BigUser.Add(ass);
                        LocalUser.Add(ass);
                    }
                }
    
            } // Сформировали из JSON List<User>
            List<string> name = new List<string>();
            ListForGridView = LocalUser;
            foreach (User user in BigUser)
            {
                name.Add(user.UserName);


            } // Сформировали лист только из названии
            List<string> nameWithoutRepetition = name.Distinct().ToList(); // Убрали повторяющиеся имена
                                                                           // nameWithoutRepetition.ToArray();
            List<UserInTable> userInTables = new List<UserInTable>();

            foreach (string a in nameWithoutRepetition)
            {
                List<UserInTable> userInTablesForAdd1 = new List<UserInTable>()
                    { new UserInTable
                {
                    UserName = a,
                    CountDay = 100,
                    SumStepsDay = 100,
                    FinishSum = 100,
                    MaxSum = 0,
                    MinSum = 0,
                }
                };
                userInTables.Add(userInTablesForAdd1[0]);

            } // Добавили в не повторяющиеся значения в новый лист объектов 
              // --------------------------**Костыль ебаный!!!-----------



            for (int i = 0; i < nameWithoutRepetition.Count(); i++)  //Дополить логику минамального и максимального
            {
                string nameA = nameWithoutRepetition[i];
                bool firstFind = false;
                int max = 0;
                int min = 0;
                foreach (User user in BigUser)
                {

                    if (nameA == user.UserName && firstFind == false)
                    {
                        userInTables[i].UserName = nameA;
                        userInTables[i].CountDay = 1;
                        userInTables[i].FinishSum = user.Steps;
                        // userInTables[i].MinSum = 10000;
                        //min = user.Steps;
                        firstFind = true;
                    }
                    if (nameA == user.UserName && firstFind == true)
                    {
                        userInTables[i].CountDay++;
                        userInTables[i].FinishSum += user.Steps;
                        if (user.Steps < min)
                        {
                            min = user.Steps;
                            userInTables[i].MinSum = min;

                        }
                        if (user.Steps > max)
                        {
                            max = user.Steps;
                            userInTables[i].MaxSum = max;
                        }

                    }

                } // заполнение нового листа для вывода в DGV
            }
      
            foreach (var user in userInTables)
            {
                user.SumStepsDay = user.FinishSum / user.CountDay;         
            } // Подсчет средного количества пройденных шагов за день
                return userInTables;         
        }  
    }
}
