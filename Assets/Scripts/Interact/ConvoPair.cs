using System.Collections.Generic;

public class Convo {
    public string speaker;
    public string text;
}

public class ConvoFlow {
    public List<Convo> convoFlow;
}

public static class ConvoPair {
    public static List<List<Convo>> ConvGreetings;
    public static List<List<Convo>> ConvKTP;
    public static List<List<Convo>> ConvNoKTP;
    public static List<List<Convo>> ConvSIM;
    public static List<List<Convo>> ConvNoSIM;
    public static List<List<Convo>> ConvSTNK;
    public static List<List<Convo>> ConvNoSTNK;
    public static List<List<Convo>> ConvVerdictGuilty;
    public static List<List<Convo>> ConvVerdictInnocent;

    const string you = "You";
    const string driver = "???";

    public static void Init() {
        InitGreetings();

        InitKTP();
        InitNoKTP();

        InitSIM();
        InitNoSIM();

        InitSTNK();
        InitNoSTNK();
    }

    public static void InitGreetings(){
        ConvGreetings = new (){
            new() { // basic convo
                new() { speaker = you,    text = "Sample striking conv 1" },
                new() { speaker = driver, text = "Responding striking conv 1" }
            },
            new() { // 
                new() { speaker = you,    text = "" },
                new() { speaker = driver, text = ""}
            },
        };
    }

    public static void InitKTP(){
        ConvKTP = new (){
            new() { // basic convo
                new() { speaker = you,    text = "KTP?" },
                new() { speaker = driver, text = "Ini, Pak"}
            },
            new() { // 
                new() { speaker = you,    text = "KTP?" },
                new() { speaker = driver, text = "Mmm.. ini, Pak"}
            },
        };
    }

    public static void InitNoKTP(){
        ConvNoKTP = new (){
            new() { // basic convo
                new() { speaker = you,    text = "KTP?" },
                new() { speaker = driver, text = "Mmmm..."},
                new() { speaker = driver, text = "Mmmm...mmm...."},
                new() { speaker = driver, text = "Maaf pak, dompet saya hilang"},
            },
            new() { // 
                new() { speaker = you,    text = "KTP?"},
                new() { speaker = driver, text = "Hahaha, saya lupa bawa pak"}
            },
        };
    }

    public static void InitSIM(){
        ConvSIM = new (){
            new() { // basic convo
                new() { speaker = you,    text = "SIM?" },
                new() { speaker = driver, text = ""}
            },
            new() { // 
                new() { speaker = you,    text = "SIM?" },
                new() { speaker = driver, text = ""}
            },
        };
    }

    public static void InitNoSIM(){
        ConvNoSIM = new (){
            new() { // basic convo
                new() { speaker = you,    text = "SIM?" },
                new() { speaker = driver, text = ""}
            },
            new() { // 
                new() { speaker = you,    text = "SIM?" },
                new() { speaker = driver, text = ""}
            },
        };
    }

    public static void InitSTNK(){
        ConvSTNK = new (){
            new() { // basic convo
                new() { speaker = you, text = "" },
                new() { speaker = driver, text = ""}
            },
            new() { // 
                new() { speaker = you, text = "" },
                new() { speaker = driver, text = ""}
            },
        };
    }

    public static void InitNoSTNK(){
        ConvNoSTNK = new (){
            new() { // basic convo
                new() { speaker = you, text = "" },
                new() { speaker = driver, text = ""}
            },
            new() { // 
                new() { speaker = you, text = "" },
                new() { speaker = driver, text = ""}
            },
        };
    }

    public static void InitVerdictGuilty() {

    }

    public static void InitVerdictInnocent() {

    }

}