using Housing.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;

namespace Housing.Extensions
{
    public static class DataSeedExtension
    {
        public static IHost SeedInitialData(this IHost host)
        {
            /*using(var scope = host.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetService<HousingContext>();
                context.Users.AddRange(
                  new CitizenUser()
                  {
                      Login = "d.ishanov@gmail.com",
                      Password = "cyganfx504",
                      AvatarUrl = "https://nekto.me/images/213000/88/photos/p_14a836d1ff.jpg",
                      Balance = 123000000
                  },
                  new CitizenUser()
                  {
                      Login = "k.saduakasov@gmail.com",
                      Password = "qalbyte666",
                      AvatarUrl = "https://upload.wikimedia.org/wikipedia/ru/thumb/2/2c/NAVI_logo.png/1335px-NAVI_logo.png",
                      Balance = 45000000
                  },
                  new CitizenUser()
                  {
                      Login = "m.shaukenov@gmail.com",
                      Password = "goldiphone322",
                      AvatarUrl = "https://keyassets-p2.timeincuk.net/wp/prod/wp-content/uploads/sites/32/2016/01/rexfeatures_3659754g.jpg",
                      Balance = 67500000
                  });
                context.SaveChanges();
            }*/
            return host;
        }
    }
}
