public class User
{
    public string login { get; set; }
}

public class Issue
{
    public int id { get; set; }

    public int number { get; set; }
    
    public User user { get; set; }
}

public class Repository
{
    public int id { get; set; }
}

public class GithubIssue
{
    public string action { get; set; }
    public Issue issue { get; set; }
    public Repository repository { get; set; }
}