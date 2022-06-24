using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorHttp.Fluent
{
    public class ServerBuilder : IServerBuilder
    {
        private List<IRequestResolver> Resolvers = new List<IRequestResolver>();
        public int Port = 8080;
        public string IP = "127.0.0.1";
        public IServerBuilder ChangePort(int PortNumber)
        {
            this.Port = PortNumber;
            return this;
        }

        public HttpServer Build()
        {
            return new HttpServer(Resolvers, this.IP, this.Port);
        }

        public IServerBuilder ChangeIp(string Ip)
        {
            this.IP = Ip;
            return this;
        }

        public IServerBuilder AddResolver(IRequestResolver resolver)
        {
            this.Resolvers.Add(resolver);
            return this;
        }

    }

    public interface IServerBuilder
    {
        public IServerBuilder ChangePort(int PortNumber);
        public IServerBuilder ChangeIp(string Ip);
        public IServerBuilder AddResolver(IRequestResolver resolver);
        public HttpServer Build();
    }
}
