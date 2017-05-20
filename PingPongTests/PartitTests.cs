using Microsoft.VisualStudio.TestTools.UnitTesting;
using PingPong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Tests
{
    [TestClass()]
    public class PartitTests
    {
        [TestMethod()]
        public void getLooserTest()
        {
            Partit partit = new Partit("key1", "player1", "key2", "player2");
            partit.punts1 = 0;
            partit.punts2 = 2;

            //comprobem si retorna el perdedor del partit
            Assert.AreEqual(partit.jugador1, partit.getLooser());
        }

        [TestMethod()]
        public void setGanadorTest()
        {
            Partit partit = new Partit("key1", "player1", "key2", "player2");
            partit.punts1 = 0;
            partit.punts2 = 2;
            partit.setGanador();
            //comproben si fa correctament l'assignació del guanyador
            Assert.AreEqual(partit.jugador2, partit.idGuanyador);
        }

        [TestMethod()]
        public void comprobarPuntsTest()
        {
            Partit partit = new Partit("key1", "player1", "key2", "player2");
            partit.punts1 = 0;
            partit.punts2 = 2;
            //comproben si la puntuació té un valor correcte
            Assert.IsTrue(partit.comprobarPunts());
        }

        [TestMethod()]
        public void puntsWithFailTest()
        {
            Partit partit = new Partit("key1", "player1", "key2", "player2");
            partit.punts1 = 0;
            partit.punts2 = 25;
            //comproben si la puntuació té un valor correcte
            Assert.IsTrue(partit.comprobarPunts());
        }
    }
}