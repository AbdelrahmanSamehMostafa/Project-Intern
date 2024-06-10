using Microsoft.EntityFrameworkCore;

public static class HotelSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>().HasData(
            new Hotel
            {
                HotelId = 1,
                AdminId = 1,
                Name = "Nile Palace Hotel",
                Rating = 4,
                averageRating = 5,
                TotalNumberOfRooms = 150,
                NumberOfAvailableRooms = 120,
                ContactInfo = "0987654321",
                AddressId = 1,
                Entertainments = new List<string> { "Pool", "Gym", "Spa" },
                Description = "A luxurious hotel with a view of the Nile.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/524850774.jpg?k=b203619100e42da199bd131cd859d3b07b22d3716a0b19f45c2d1efed3fe6ec1&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/520473485.jpg?k=1bf83b123f220000211613f553daed1cdd5215cb9b9d0c9fb2b02cf37e77f53b&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/520473637.jpg?k=1cdff3a2caf2bc09c841ce9a506e93e497d9884c97dfeabf348a980f4ee7c928&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/524852168.jpg?k=bf8c176cf3ed0609bffd758ae84e93c0afc23c3fa686cc502e3608f4e6e28ba4&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 2,
                AdminId = 1,
                Name = "Pyramids View Hotel",
                Rating = 4.5,
                averageRating = 0,
                TotalNumberOfRooms = 200,
                NumberOfAvailableRooms = 180,
                ContactInfo = "1112223333",
                AddressId = 2,
                Entertainments = new List<string> { "Pool", "Gym", "Restaurant" },
                Description = "Experience the pyramids from the comfort of your room.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/132392578.jpg?k=36f408eab4b81e6b93908f97bf948d9498e617ddd717c9b47a865accfd9ef034&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/134958505.jpg?k=144659ef64f3395d19a2e047fee7e492956aeb8d0c5a39e25edcd9d081012def&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/230095386.jpg?k=42c421355a2f88155e013adcd4fa47223ed925eba426c42c6c00347b3134e4c4&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 3,
                AdminId = 2,
                Name = "Solymar Soma Beach",
                Rating = 3.5,
                TotalNumberOfRooms = 120,
                averageRating = 0,
                NumberOfAvailableRooms = 100,
                ContactInfo = "4445556666",
                AddressId = 3,
                Entertainments = new List<string> { "Pool", "Beach Access", "Diving Center" },
                Description = "A resort with stunning views of the Red Sea.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/277906335.jpg?k=0728b13b2796fe7938cad934580ed9680335b065f15bf454c01d4bd6274a378e&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/277906344.jpg?k=ea9def902741b4cb697fea8e63e3973284ad934151d2a21adb8b5ee3b5aac483&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 4,
                AdminId = 2,
                Name = "Pyramisa Hotel Luxor",
                Rating = 4,
                averageRating = 0,
                TotalNumberOfRooms = 180,
                NumberOfAvailableRooms = 150,
                ContactInfo = "7778889999",
                AddressId = 4,
                Entertainments = new List<string> { "Pool", "Gym", "Cultural Shows" },
                Description = "Stay at the heart of ancient Luxor.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581584.jpg?k=f58890637be490f3c2ac308b6477fcfd39318c1645aa8955965c46e37dfe0cd6&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581592.jpg?k=4828f1841a092779d151c19ac5cf1858f89fc8cd9ec616f52587820158e2582a&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581589.jpg?k=da6d3f9a30c7d8260c37396bc3b4a154f52b586d0cb0058644cb6eb96547d3ea&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 5,
                AdminId = 2,
                Name = "Kato Dool Wellness Aswan Resort",
                Rating = 3.5,
                averageRating = 0,
                TotalNumberOfRooms = 100,
                NumberOfAvailableRooms = 80,
                ContactInfo = "9990001111",
                AddressId = 5,
                Entertainments = new List<string> { "Pool", "Spa", "Nile Cruise" },
                Description = "Relax and unwind at the serene Aswan Retreat.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/426899015.jpg?k=bf203184a29ba54a17019673775e942257d9a104d37edd5bd70c21d1c14df131&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/433682205.jpg?k=54b4255731815f8d441aba48caff95b42978da7418f9910c2f449ec14b04f731&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/433682175.jpg?k=ca637a1a53f9f59cce26359a09b50d49a02d6f89475581fe23b55f54beb16847&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/426526344.jpg?k=58a01e11e894798d38595a17f834ae4db31bc20ccc12ce6a268ce29443f31ccc&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/346689229.jpg?k=765e02a4817f6d94e9262571c3de6aa2b6b0e8809caf660ca0a6532f6e7b01f0&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 6,
                AdminId = 1,
                Name = "Davinci Beach Hotel",
                Rating = 6,
                averageRating = 0,
                TotalNumberOfRooms = 90,
                NumberOfAvailableRooms = 50,
                ContactInfo = "9990001111",
                AddressId = 6,
                Entertainments = new List<string> { "Pool", "Spa", "Cinema", "Gym" },
                Description = "Relax and unwind at the serene Aswan Retreat.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/537868801.jpg?k=0be00f52a11ffb2ab5b0fa76d9c240b6216281d37325733b4d9f4c47c33316ca&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/538108568.jpg?k=b4056a34bd591de23ea755e29ebed5ac49d442697d1e0cd8770fecb2150ec780&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/540489415.jpg?k=988a95c60f3b8fa03eb98d8de7df0ba41e8e3e8c4f0b3f7a6666c898ddbd16c4&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/540468991.jpg?k=8fb435baa87c07a4886ebd70219647d49f4098a33664cef7a19daefe36bf3138&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 7,
                AdminId = 2,
                Name = "Ghazala Gardens",
                Rating = 5,
                averageRating = 0,
                TotalNumberOfRooms = 190,
                NumberOfAvailableRooms = 180,
                ContactInfo = "9990001111",
                AddressId = 7,
                Entertainments = new List<string> { "Pool", "Spa", "Nile Cruise", "Gym" },
                Description = "Relax and unwind at the serene Aswan Retreat.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476642414.jpg?k=b299afe8fb788d2f07ed8d455d2a5ee108b7aa9d1dd158bce7f6e8594b6b23da&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/469282264.jpg?k=ffbb57a1619377bfc2d9798bef37560a69c95c430c1c212163862f7ca1fb322d&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476642883.jpg?k=0829695a81fb80a84ef6351e0b2d0e65650de7c2e8b53612c4f28f15c5e737a3&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 8,
                AdminId = 2,
                Name = "Lasirena Mini Egypt",
                Rating = 4.2,
                averageRating = 0,
                TotalNumberOfRooms = 110,
                NumberOfAvailableRooms = 40,
                ContactInfo = "9990001111",
                AddressId = 8,
                Entertainments = new List<string> { "Pool", "Spa", "Cinema", "Gym", "Beach" },
                Description = "Relax and unwind at the serene Aswan Retreat.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114940.jpg?k=9797a229e13a6051352066b66ffd1ec6ba1b72e98491a49cff2e2355f643ed8a&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114935.jpg?k=8babd6e32091e5b4cf72d6a3d841c5d4657c5313f5d5e0edee64dc84f89598c5&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114978.jpg?k=3e81221800f42cf658213d7fcf2f70d9fdbf2f2e92d3db1d753ff90b39b605fa&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114996.jpg?k=ecb0e70288f4faba4fde7587f319c4c67729a59bdbfce6eb0596ac50c8127ad9&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114877.jpg?k=972b540ac1e6a145d237c70de8774911874a9ab5dff26165ee607352866f9fef&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 9,
                AdminId = 1,
                Name = "Nozha Beach",
                Rating = 6.7,
                averageRating = 0,
                TotalNumberOfRooms = 130,
                NumberOfAvailableRooms = 70,
                ContactInfo = "9990001111",
                AddressId = 9,
                Entertainments = new List<string> { "Pool", "Cinema", "Gym", "Beach" },
                Description = "Relax and unwind at the serene Aswan Retreat.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/535840098.jpg?k=79166f7ca744f0c6d60d45a1133ee53e383c91821ceb2da87fc20039ed86cfa9&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/541431499.jpg?k=5b0997797e626a7a60b30ff4af5a48bfd31b04946cf03e40634e2b4af9a7a998&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/541433214.jpg?k=7eb3ed246be88900fc95b3113c9d38fcb0c3bc1bae863e7d6938e14dfd84b5b2&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/542945965.jpg?k=4d8266ddb1c72ea2167c67fe11b88f98e5c914e817ce7cfc09b0be941128c0bd&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/541251754.jpg?k=0229dc6a0187d48f92635e7a14d1ba1e1c90b42affee2128ee0fea8051a60960&o=&hp=1",
                },
                IsActive = true
            }
        );
    }
}
