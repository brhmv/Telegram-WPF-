using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telegram
{
    public class Person
    {
        public string? Name { get; set; }

        public string? Image { get; set; }

        public ObservableCollection<string?>? Messages { get;  set; } =new();

        public Person() { }
    }
}
