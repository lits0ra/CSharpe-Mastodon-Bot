using Mastonet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraMastodon
{
    class Mastodon
    {
        static void Main(string[] args)
        {
            var mastodon = new Mastodon();
            mastodon.Streaming();

            Console.ReadKey();
        }

        private async Task Streaming()
        {
            var authClient = new AuthenticationClient("pawoo.net");
            var appRegistration = await authClient.CreateApp("s0ratest", Scope.Read | Scope.Write | Scope.Follow);

            var auth = await authClient.ConnectWithPassword("Email", "Password");

            var client = new MastodonClient(appRegistration, auth);

            var streaming = client.GetPublicStreaming();
            streaming.OnUpdate += (sender, e) =>
            {
                Console.WriteLine(e.Status.Content);
;               Console.WriteLine(e.Status.Id);
                Console.WriteLine(e.Status.Language);
                Console.WriteLine(e.Status.FavouritesCount);
                Console.WriteLine(e.Status.CreatedAt);
                Console.WriteLine();
            };

            await streaming.Start();
        }
    }
}