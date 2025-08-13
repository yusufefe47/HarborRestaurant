using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Context;

public class UpdateTranslations
{
    public static void UpdateAllTranslations()
    {
        var connectionString = "Server=(localdb)\\mssqllocaldb;Database=HarborRestaurant;Trusted_Connection=true;MultipleActiveResultSets=true";
        
        var optionsBuilder = new DbContextOptionsBuilder<HarborDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        
        using (var context = new HarborDbContext(optionsBuilder.Options))
        {
            // Update Rooms
            var rooms = context.Rooms.ToList();
            foreach (var room in rooms)
            {
                switch (room.RoomId)
                {
                    case 1:
                        room.NameEn = "Deluxe Sea View Room";
                        room.DescriptionEn = "Spacious room with stunning sea view, private balcony and modern amenities. Perfect for couples seeking luxury and comfort.";
                        break;
                    case 2:
                        room.NameEn = "Premium Suite";
                        room.DescriptionEn = "Luxurious suite with separate living area, panoramic sea view and exclusive services. Ideal for special occasions.";
                        break;
                    case 3:
                        room.NameEn = "Family Room";
                        room.DescriptionEn = "Comfortable family room with multiple beds, children's area and all necessary amenities for a perfect family vacation.";
                        break;
                    case 4:
                        room.NameEn = "Standard Room";
                        room.DescriptionEn = "Cozy and comfortable room with modern amenities, perfect for business travelers and couples.";
                        break;
                    case 5:
                        room.NameEn = "Garden View Room";
                        room.DescriptionEn = "Peaceful room overlooking the beautiful garden, offering tranquility and comfort away from the city noise.";
                        break;
                    case 6:
                        room.NameEn = "Executive Suite";
                        room.DescriptionEn = "Premium executive suite with office area, meeting facilities and exclusive business services.";
                        break;
                }
            }
            
            // Update MenuItems
            var menuItems = context.MenuItems.ToList();
            foreach (var item in menuItems)
            {
                switch (item.ItemId)
                {
                    case 1:
                        item.NameEn = "Grilled Sea Bass";
                        item.DescriptionEn = "Fresh sea bass grilled to perfection, served with seasonal vegetables and lemon butter sauce.";
                        break;
                    case 2:
                        item.NameEn = "Mediterranean Seafood Platter";
                        item.DescriptionEn = "A selection of fresh seafood including shrimp, calamari, and mussels with Mediterranean herbs.";
                        break;
                    case 3:
                        item.NameEn = "Lamb Chops";
                        item.DescriptionEn = "Tender lamb chops marinated with Turkish spices, grilled and served with roasted vegetables.";
                        break;
                    case 4:
                        item.NameEn = "Stuffed Eggplant";
                        item.DescriptionEn = "Traditional Turkish stuffed eggplant with ground meat, onions, and aromatic spices.";
                        break;
                    case 5:
                        item.NameEn = "Turkish Breakfast";
                        item.DescriptionEn = "Complete Turkish breakfast with cheese, olives, tomatoes, cucumbers, honey and fresh bread.";
                        break;
                    case 6:
                        item.NameEn = "Baklava";
                        item.DescriptionEn = "Traditional Turkish dessert made with layers of phyllo pastry, nuts and honey syrup.";
                        break;
                    case 7:
                        item.NameEn = "Turkish Coffee";
                        item.DescriptionEn = "Authentic Turkish coffee served with Turkish delight, a UNESCO recognized cultural heritage.";
                        break;
                    case 8:
                        item.NameEn = "Meze Platter";
                        item.DescriptionEn = "Assorted Turkish appetizers including hummus, ezme, cacik and stuffed grape leaves.";
                        break;
                    case 9:
                        item.NameEn = "Grilled Octopus";
                        item.DescriptionEn = "Tender grilled octopus with olive oil, lemon and fresh herbs, a Mediterranean delicacy.";
                        break;
                }
            }
            
            // Update BlogPosts
            var blogPosts = context.BlogPosts.ToList();
            foreach (var post in blogPosts)
            {
                switch (post.PostId)
                {
                    case 1:
                        post.TitleEn = "New Spring Menu";
                        post.SummaryEn = "Discover our new spring menu featuring fresh seasonal ingredients and innovative flavors.";
                        post.ContentEn = "We are excited to introduce our new spring menu, carefully crafted by our expert chefs using the freshest seasonal ingredients. From Mediterranean seafood to traditional Turkish delicacies, our new offerings promise to delight your taste buds with innovative flavors and beautiful presentations.";
                        break;
                    case 2:
                        post.TitleEn = "Special Valentine's Day Event";
                        post.SummaryEn = "Join us for a romantic Valentine's Day dinner with special menu and live music.";
                        post.ContentEn = "Celebrate love at Harborlights this Valentine's Day with our specially designed romantic dinner experience. Enjoy a candlelit dinner with our exclusive Valentine's menu, featuring aphrodisiac ingredients and exquisite wines, accompanied by live acoustic music.";
                        break;
                    case 3:
                        post.TitleEn = "Chef's Table Experience";
                        post.SummaryEn = "Book your seat at our exclusive chef's table for an unforgettable culinary journey.";
                        post.ContentEn = "Experience the art of cooking up close with our Chef's Table experience. Watch our master chefs prepare your meal while explaining the techniques and stories behind each dish. This intimate dining experience is limited to 8 guests per evening.";
                        break;
                }
            }
            
            context.SaveChanges();
            Console.WriteLine("All translations updated successfully!");
        }
    }
}
