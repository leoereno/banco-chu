namespace BancoChu.Services
{
    public class UuidServices
    {
        public static Guid NewId() => UUIDNext.Uuid.NewDatabaseFriendly(UUIDNext.Database.Other);
    }
}
