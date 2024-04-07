class User
{
    public static int userID;

    public static void SignIn()
    {
        // User authentication 
        userID = 1; // Example ID
    }

    public static int GetUserID()
    {
        return userID;
    }
}