using System.Collections.Generic;
using UnityEngine;

public class Convo {
    public string speaker;
    public string text;
}
public class ConvoFlow {
    public List<Convo> flow;
}

public class ConvoPair : MonoBehaviour {

    public List<ConvoFlow> ConvGreetings;
    public List<ConvoFlow> ConvBasicChat;
    public List<ConvoFlow> ConvKTP;
    public List<ConvoFlow> ConvNoKTP;
    public List<ConvoFlow> ConvSIM;
    public List<ConvoFlow> ConvNoSIM;
    public List<ConvoFlow> ConvSTNK;
    public List<ConvoFlow> ConvNoSTNK;
    public List<ConvoFlow> ConvVerdictGuilty;
    public List<ConvoFlow> ConvVerdictInnocent;


    const string you = "You";
    const string driver = "???";

    void Awake()
    {
        InitGreetings();
        InitBasicChat();

        InitKTP();
        InitNoKTP();

        InitSIM();
        InitNoSIM();

        InitSTNK();
        InitNoSTNK();

        InitVerdictGuilty();
        InitVerdictInnocent();
    }

    public void InitGreetings() {
        ConvGreetings = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Sample Greetings" },
                    new (){ speaker = driver, text = "Responding to sample greetings" }
                }
            },
        };
    }

    public void InitBasicChat(){
        ConvBasicChat = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Sample striking basic conv 1" },
                    new (){ speaker = driver, text = "Responding striking basic conv 1" }
                }
            },
            // new() { // 
            //     new (){ speaker = you,    text = "" },
            //     new (){ speaker = driver, text = ""}
            // },
        };
    }

    public void InitKTP(){
        ConvKTP = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "KTP?" },
                    new (){ speaker = driver, text = "Ini, Pak"}
                }
            },
            new() {
                flow = new() {
                    new (){ speaker = you,    text = "KTP?" },
                    new (){ speaker = driver, text = "Mmm.. ini, Pak"}
                }
            },
        };
    }

    public void InitNoKTP(){
        ConvNoKTP = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "KTP?" },
                    new (){ speaker = driver, text = "Mmmm..."},
                    new (){ speaker = driver, text = "Mmmm...mmm...."},
                    new (){ speaker = driver, text = "Maaf pak, dompet saya hilang"},
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "KTP?"},
                    new (){ speaker = driver, text = "Hahaha, saya lupa bawa pak"}
                }
            },
        };
    }

    public void InitSIM(){
        ConvSIM = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "SIM?" },
                    new (){ speaker = driver, text = ""}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "SIM?" },
                    new (){ speaker = driver, text = ""}
                }
            },
        };
    }

    public void InitNoSIM(){
        ConvNoSIM = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "SIM?" },
                    new (){ speaker = driver, text = ""}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "SIM?" },
                    new (){ speaker = driver, text = ""}
                }
            },
        };
    }

    public void InitSTNK(){
        ConvSTNK = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "STNK?" },
                    new (){ speaker = driver, text = ""}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "STNK?" },
                    new (){ speaker = driver, text = ""}
                }
            },
        };
    }

    public void InitNoSTNK(){
        ConvNoSTNK = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "STNK?" },
                    new (){ speaker = driver, text = ""}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "STNK?" },
                    new (){ speaker = driver, text = ""}
                }
            },
        };
    }

    public void InitVerdictGuilty() {
        ConvVerdictGuilty = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Kesimpulannya" },
                    new (){ speaker = driver, text = ".."}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Kesimpullannya" },
                    new (){ speaker = driver, text = ".."}
                }
            },
        };
    }

    public void InitVerdictInnocent() {
        ConvVerdictGuilty = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Kesimpulannya" },
                    new (){ speaker = driver, text = ".."}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Kesimpullannya" },
                    new (){ speaker = driver, text = ".."}
                }
            },
        };
    }

}
