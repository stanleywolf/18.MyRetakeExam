using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Data
{
   public class Engine
   {
       protected StorageMaster storageMaster;

       public Engine()
       {
           this.storageMaster = new StorageMaster();
       }

       public void Run()
       {
           string input;
           while ((input = Console.ReadLine()) != "END")
           {
               try
               {

                   var splitedCommand = input.Split();
                   string command = splitedCommand[0];
                   var tokens = splitedCommand.Skip(1).ToArray();

                   if (string.IsNullOrEmpty(command))
                   {
                       return;
                   }
                   var output = string.Empty;
                   switch (command)
                   {
                       case "AddProduct":
                           output = storageMaster.AddProduct(tokens[0], double.Parse(tokens[1]));
                           break;
                       case "RegisterStorage":
                           output = storageMaster.RegisterStorage(tokens[0], tokens[1]);
                           break;
                       case "SelectVehicle":
                           output = storageMaster.SelectVehicle(tokens[0], int.Parse(tokens[1]));
                           break;
                        case "LoadVehicle":
                            output = storageMaster.LoadVehicle(tokens);
                            break;
                        case "SendVehicleTo":
                           output = storageMaster.SendVehicleTo(tokens[0], int.Parse(tokens[1]), tokens[2]);
                           break;
                       case "UnloadVehicle":
                           output = storageMaster.UnloadVehicle(tokens[0], int.Parse(tokens[1]));
                           break;
                       case "GetStorageStatus":
                           output = storageMaster.GetStorageStatus(tokens[0]);
                           break;
                            
                   }
                   if (output != String.Empty)
                   {
                       Console.WriteLine(output);
                   }
               }
               catch (InvalidOperationException e)
               {
                   Console.WriteLine("Error: " + e.Message);
               }


           }
        }
   }
}
