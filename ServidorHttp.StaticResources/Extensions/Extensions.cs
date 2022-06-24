
namespace ServidorHttp.StaticResources.Extensions
{
    public static class Extensions
    {
        public static IServerBuilder UseStaticResource(this IServerBuilder builder, Action<StaticResourceOptions> options)
        {
            var s = new StaticResourceBuilder();
            options(s);
            var parserChosed = s.ParserChosed ?? new SinglePathParser(s.BasePath);
            builder.AddResolver(new StaticResourceResolver(parserChosed));
            return builder;
        }

        private class StaticResourceBuilder : StaticResourceOptions, MultipleScopeBuilder
        {
            public string BasePath { get; set; } = Environment.CurrentDirectory;
            public StaticResourcePathParser? ParserChosed = null;
            public MultipleScopeBuilder UseDiferentsFoldersForDiferentHosts()
            {
                this.ParserChosed = new ScopedPathParser(BasePath);
                return this;
            }
            public MultipleScopeBuilder AddPathForHost(string HostName, string RelativePath)
            {
                (this.ParserChosed as ScopedPathParser ?? throw new InvalidOperationException()).AddScopedHost(HostName, RelativePath);
                return this;
            }
        }
        public interface StaticResourceOptions
        {
            public string BasePath { get; set; }
            public MultipleScopeBuilder UseDiferentsFoldersForDiferentHosts();
        }
        public interface MultipleScopeBuilder
        {
            public MultipleScopeBuilder AddPathForHost(string HostName, string RelativePath);
        }

    }

}