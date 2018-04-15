using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WebBore.FtpClient;
using System.Net;

namespace FtpClientTest
{
    [TestFixture]
    public class TestClient
    {

        private string username = "";
        private string password = "";

        [TestCase,Ignore("")]
        public void ConnectTest()
        {
            using (FTPSClient client = new FTPSClient())
            {
                // Connect to the server, with mandatory SSL/TLS 
                // encryption during authentication and 
                // optional encryption on the data channel 
                // (directory lists, file transfers)
                try
                {
                    // client.Connect("us1.DOMAIN.com", new NetworkCredential(read-write, password), ESSLSupportMode.CredentialsRequired | ESSLSupportMode.DataChannelRequested);
                    // client.Connect("us1.DOMAIN.com", new NetworkCredential(read-write, password), ESSLSupportMode.CredentialsRequired | ESSLSupportMode.DataChannelRequested);
                    // client.Connect("us1.DOMAIN.com", new NetworkCredential(read-write, password), ESSLSupportMode.ControlAndDataChannelsRequired);
                    // client.Connect("ftp.DOMAIN.com", new NetworkCredential(read-write, password), ESSLSupportMode.ClearText);
                    client.Connect("ftps.DOMAIN.com", new NetworkCredential("username", password), ESSLSupportMode.ControlAndDataChannelsRequested);

                    // client.MakeDir(System.Guid.NewGuid().ToString());
                    var sys = client.GetSystem();
                    var current = client.GetCurrentDirectory();
                    // client.SetCurrentDirectory("/Directory");
                    // client.GetFile("/Directory/File.ext", "c:\\temp\\File.ext");

                }
                catch (Exception ex)
                {
                    // handle exception
                }
                finally
                {
                    client.Close();
                }
            }
        }
    }
}

