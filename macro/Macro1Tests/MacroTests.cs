using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class MacroTests
    {
        [TestMethod()]
        public void StrokaZadaniyaTest_1900_1_1_00_00_00_return_chet()
        {
            DateTime date = new DateTime(1900, 1, 1, 0, 0, 0);
            String Result = ClientObjectHelpers.StrokaZadaniya(date);
            Assert.AreEqual(Result, "чет!");
        }
        [TestMethod()]
        public void StrokaZadaniyaTest_9999_12_31_23_59_59_return_nechet()
        {
            DateTime date = new DateTime(9999, 12, 31, 23, 59, 59);
            String Result = ClientObjectHelpers.StrokaZadaniya(date);
            Assert.AreEqual(Result, "нечет!");
        }
        [TestMethod()]
        public void StrokaZadaniyaTest_2024_09_19_13_15_00_return_ravno()
        {
            DateTime date = new DateTime(2024, 9, 19, 13, 15, 00);
            String Result = ClientObjectHelpers.StrokaZadaniya(date);
            Assert.AreEqual(Result, "равно!");
        }

        [TestMethod]
        public void Server_Started_Client_Connected_localhost_8888()
        {
            ServerObject server = new ServerObject();// создаем сервер
            server.ListenAsync(); // запускаем сервер
            var tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 8888);
            Assert.IsTrue(tcpClient.Connected);
        }
    }
}