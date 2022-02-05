using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Song
    {
        public string Name { get; set; }
        public string Path { get; set; } 

        public Song()
        {

        }

        public Song(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }
    }
}
