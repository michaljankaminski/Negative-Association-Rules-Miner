using Negative_Association_Rules_Miner.model.mining;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Negative_Association_Rules_Miner.output;

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
            int option = GetOption(Console.ReadLine());
            if (option < 1 || option > 6)
            {
                Console.WriteLine("The chosen option is incorrect. Try again.");
                Start();
            }
            else
            {
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
            int option = GetOption(Console.ReadLine());
            Console.WriteLine("");
            if (option < 0 || option >= numOfItems)
            {
                Console.WriteLine("The chosen option is incorrect. Try again.");
            }
            else
            {
                int selectedSource = option;
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
            int option = GetOption(Console.ReadLine());
            if (option == 0)
            {
                Start();
                return;
            }
            else if (option < 0 || option > 2)
            {
                Console.WriteLine("The chosen option is incorrect. Try again.");
            }
            else
            {
                switch (option)
                {
                    case 0:
                        break;
                    case 1:
                        IncludeItems();
                        break;
                    case 2:
                        ExcludeItems();
                        break;
                }
            }
            Start();
        }

        private void IncludeItems()
        {
            Console.WriteLine("Write what columns do you want to include (example: coffee;tea)");
            var items = Console.ReadLine();
            manager.IncludeItems(items.Split(';'));
        }

        private void ExcludeItems()
        {
            Console.WriteLine("Write what columns do you want to exclude (example: coffee;tea)");
            var items = Console.ReadLine();
            manager.ExcludeItems(items.Split(';'));
        }

        private void MineRules()
        {
            if (!sourceSelected)
            {
                Console.WriteLine("THe data source was not chosen.");
                Start();
                return;
            }

            try
            {
                Console.WriteLine("Please enter a minimal support (for example:0,4).");
                var supp = GetNumber(Console.ReadLine());
                if (supp < 0)
                    throw new Exception("Wpisano nie poprawną wartość.");

                Console.WriteLine("Please enter a minimal confidence (for example:0,2).");
                var conf = GetNumber(Console.ReadLine());
                if (conf < 0)
                    throw new Exception("Wpisano nie poprawną wartość.");

                Console.WriteLine("Please enter a minimal rule length (for example:2).");
                var minlen = GetOption(Console.ReadLine());
                if (minlen < 0)
                    throw new Exception("Wpisano nie poprawną wartość.");

                Console.WriteLine("Please enter a maximal rule length (for example:4).");
                var maxlen = GetOption(Console.ReadLine());
                if (maxlen < 0)
                    throw new Exception("Wpisano nie poprawną wartość.");

                RuleParameters param = new RuleParameters
                {
                    MinSupport = supp,
                    MinConfidence = conf,
                    MinLength = minlen,
                    MaxLength = maxlen
                };

                Console.WriteLine("Which approach of mining do you want to use?(1 or 2)");
                var opt = GetOption(Console.ReadLine());
                if (opt < 1 || opt > 2)
                    throw new Exception("Wpisano nie poprawną wartość.");

                manager.GetObservableRulesCollection().CollectionChanged += MinerConsole_CollectionChanged;
                Console.WriteLine("Mining is being processed...");
                if (opt == 1)
                {
                    manager.FindRule(param);
                }
                else if (opt == 2)
                {
                    manager.FindRuleSecondApproach(param);
                }

                if (manager.GetObservableRulesCollection().Count == 0)
                    Console.WriteLine("No results found");
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Mining has finished!");
                    Console.WriteLine("");
                }

                Console.WriteLine("");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Start();
            }
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

            lhs = lhs.Substring(0, lhs.Length - 1);
            rhs = rhs.Substring(0, rhs.Length - 1);
            switch (lastFoundRule.Type)
            {
                case RuleType.BothNegative:
                    lhs = "¬ " + lhs;
                    rhs = "¬ " + rhs;
                    break;
                case RuleType.LeftNegative:
                    lhs = "¬ " + lhs;
                    break;
                case RuleType.RightNegative:
                    rhs = "¬ " + rhs;
                    break;
            }
            
            Console.WriteLine(string.Format("{0} => {1}   (Support = {2}, Confidence = {3})",
                lhs, rhs, Math.Round(lastFoundRule.Support,2),Math.Round(lastFoundRule.Confidence,2)));
        }

        private int GetOption(string input)
        {
            input = input.Substring(0, 1);
            if (int.TryParse(input, out int option))
            {
                return option;
            }

            return -1;
        }

        private double GetNumber(string input)
        {
            if (double.TryParse(input, out double result))
            {
                return result;
            }

            return -1d;
        }
    }
}
