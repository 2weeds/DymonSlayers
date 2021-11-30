using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlyweightTest
{
    public partial class FormGame : Form
    {
        Creator lvCreator = new LevelCreator();
        LevelObject level;
        AbstractFactory objectFactory;
        Random rnd = new Random();
        public FormGame()
        {
            InitializeComponent();
            checkMemory();
        }

        public void checkMemory()
        {
            long kbAtExecution = GC.GetTotalMemory(false) / 1024;
            for (int i = 1; i <= 3; i++)
            {
                level = lvCreator.factoryMethod(i);
                objectFactory = level.getAbstractFactory();
                Unit mapObj = objectFactory.createGunBullets();
                for (int j = 0; j < 1000; j++)
                {
                    mapObj.spawnUnit(this, rnd.Next(10, 300), rnd.Next(10, 300));
                }
            }
            long kbAfterExecution = GC.GetTotalMemory(false) / 1024;
            Console.WriteLine("Before {0} and After {1}", kbAtExecution, kbAfterExecution);
        }
    }
}
