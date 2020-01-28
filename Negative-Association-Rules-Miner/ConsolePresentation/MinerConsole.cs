using Negative_Association_Rules_Miner;
using Negative_Association_Rules_Miner.model.mining;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ConsolePresentation
{
    class MinerConsole
    {
        private readonly MinerManager manager;
        private bool sourceSelected = false;
        public MinerConsole()
        {
            manager = new MinerManager();
            Console.WriteLine("Negative association rules miner.");
        }

        public void MinerConsole_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var collection = (sender as ObservableCollection<Rule>);
            ShowLastFoundRule(collection.Last());
        }

        public void Start()
        {
            Console.WriteLine("");
            Console.WriteLine("Available options:");
            Console.WriteLine("1. List available datasets in local storage.");
            Console.WriteLine("2. Add new file to storage.");
            Console.WriteLine("3. Load dataset to memory.");
            Console.WriteLine("4. Filter attributes in dataset.");
            Console.WriteLine("5. Mine negative rules.");
            Console.WriteLine("Select one of available option.");
            var key = Console.Read();
            Console.WriteLine("");
            if (key < 49 || key > 53)
            {
                Console.WriteLine("The chosen option is incorrect. Try again.");
                Start();
                return;
            }
            else
            {
                int option = key - 48;
                switch (option)
                {
                    case 1:
                        ListSources();
                        Start();
                        break;
                    case 2:
                        AddNewSource();
                        break;
                    case 3:
                        SelectSource();
                        break;
                    case 4:
                        FilterDataset();
                        break;
                    case 5:
                        MineRules();
                        break;
                    default:
                        break;
                }
            }
        }
        private void ListSources()
        {
            Console.WriteLine("");
            Console.WriteLine("Available sources:");
            var items = manager.ViewAvailableSources().ToList();
            if (items.Count == 0)
            {
                Console.WriteLine("There are no available files.");
            }
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        private void AddNewSource()
        {
            Console.WriteLine("Please submit a path to file:");
            var path = Console.ReadLine();
            if (!File.Exists(path))
            {
                Console.WriteLine("Path is incorrect. Try again.");
                AddNewSource();
            }

            var res = manager.AddNewSource(path);
            if (!res)
            {
                Console.WriteLine("Adding new file was unsuccessful.");
            }
            else
            {
             
                Console.WriteLine("File was added successfully.");
                ListSources();
            }
            Start();
        }
        private void SelectSource()
        {
            ListSources();
            Console.WriteLine("");
            Console.WriteLine("Select one of the sources:");
            var numOfItems = manager.ViewAvailableSources().Count();
            if (numOfItems == 0)
            {
                Console.WriteLine("There no files available.");
                Start();
                return;
            }
            var key = Console.Read();
            Console.WriteLine("");
            if (key < 48 || key > numOfItems + 48)
            {
                Console.WriteLine("The chosen option is incorrect. Try again.");
            }
            else
            {
                int selectedSource = key - 48;
                if (manager.SelectSource(selectedSource))
                {
                    Console.WriteLine("Dataset was loaded successfully.");
                    sourceSelected = true;
                    ListCurrentHeaders();
                }
                else
                {
                    Console.WriteLine("Loading dataset was unsuccessful.");
                }
            }
            Start();
        }

        private void FilterDataset()
        {
            ListCurrentHeaders();
            Console.WriteLine("");
            Console.WriteLine("Select available option:");
            Console.WriteLine("0. Return to start");
            Console.WriteLine("1. Include items which we want to mine.");
            Console.WriteLine("2. Exclude items which we do not want to include.");
            var key = Console.Read();
            Console.WriteLine("");
            if (key < 48 || key > 50)
            {
                Console.WriteLine("The chosen option is incorrect. Try again.");
                Start();
                return;
            }
            else
            {
                throw new NotImplementedException();
                //TODO: To implement filtering
                int option = key - 48;
                switch (option)
                {
                    case 0:
                        break;
                    case 1:
                        AddNewSource();
                        break;
                    case 2:
                        SelectSource();
                        break;
                }
            }
            Start();
        }

        private void MineRules()
        {
            manager.GetObservableRulesCollection().CollectionChanged += MinerConsole_CollectionChanged;
            throw new NotImplementedException();
        }

        private void ListCurrentHeaders()
        {
            var items =manager.GetListOfHeaders();
            Console.WriteLine("List of attributes:");
            string output = string.Empty;
            foreach (var item in items)
            {
                output += item + ";";
            }
            Console.WriteLine(output);
        }

        public void ShowLastFoundRule(Rule lastFoundRule)
        {
            string lhs = string.Empty;
            string rhs = string.Empty;
            foreach (var lsingle in lastFoundRule.LeftItemSet)
            {
                lhs += lsingle.Name + ";";
            }
            foreach (var rsingle in lastFoundRule.RightItemSet)
            {
                rhs += rsingle.Name + ";";
            }
            //Logger.Log(string.Format("{0} => ~~ {1}",
            //    string.Join(" ,", lhsEl.Select(r => r.Name)),
            //    string.Join(" ,", rhs.Select(r => r.Name))));
            Console.WriteLine(string.Format("{0} => {1}",
                lhs, rhs));
        }
    }
}
