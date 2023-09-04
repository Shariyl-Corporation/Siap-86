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

    public List<ConvoFlow> ConvGreetingsInnocent;
    public List<ConvoFlow> ConvGreetingsGuilty;
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
        InitGreetingsInnocent();
        InitGreetingsGuilty();
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

    public void InitGreetingsInnocent() {
        ConvGreetingsInnocent = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Selamat pagi, Anda tahu kenapa anda diberhentikan?" },
                    new (){ speaker = driver, text = "Saya tidak tahu pak, saya merasa tidak melakukan hal yang salah" }
                }
            },
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Selamat pagi, tolong matikan mesin Anda" },
                    new (){ speaker = driver, text = "Baik pak, kenapa saya diberhentikan?" }
                }
            },
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Selamat pagi, saya perlu melihat kelengkapan surat-surat Anda" },
                    new (){ speaker = driver, text = "Baik pak polisi" }
                }
            }
        };
    }

    public void InitGreetingsGuilty() {
        ConvGreetingsGuilty = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Selamat pagi, Anda tahu kenapa Anda diberhentikan?" },
                    new (){ speaker = driver, text = "Kenapa pak?" }
                }
            },
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Selamat pagi, saya melihat Anda melanggar lalu lintas, tolong siapkan surat-surat Anda" },
                    new (){ speaker = driver, text = "Maaf pak, baik saya siapkan" }
                }
            },
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Selamat pagi, saya telah mencatat Anda melanggar lalu lintas, siapkan surat-surat Anda" },
                    new (){ speaker = driver, text = "..." }
                }
            }
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
                    new (){ speaker = you,    text = "Bisa saya lihat KTP Anda?" },
                    new (){ speaker = driver, text = "Ini, Pak"}
                }
            },
            new() {
                flow = new() {
                    new (){ speaker = you,    text = "Tolong siapkan KTP Anda" },
                    new (){ speaker = driver, text = "Mmm.. ini, Pak"}
                }
            },
        };
    }

    public void InitNoKTP(){
        ConvNoKTP = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Bisa saya lihat KTP Anda?" },
                    new (){ speaker = driver, text = "Mmmm.."},
                    new (){ speaker = driver, text = "Maaf pak, dompet saya ketinggalan"},
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Mana KTP Anda?"},
                    new (){ speaker = driver, text = "Hahaha, saya lupa bawa pak"}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Perlihatkan KTP Anda"},
                    new (){ speaker = driver, text = "Mohon maaf pak, KTP saya tertinggal di rumah"}
                }
            },
        };
    }

    public void InitSIM(){
        ConvSIM = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Dimana SIM Anda?" },
                    new (){ speaker = driver, text = "Sebentar pak, saya ambilkan.."}
                    new (){ speaker = driver, text = "Ini SIM saya"}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Bisa perlihatkan SIM Anda?" },
                    new (){ speaker = driver, text = "Ini SIM saya pak"}
                }
            }
        };
    }

    public void InitNoSIM(){
        ConvNoSIM = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Bisa tunjukkan SIM Anda?" },
                    new (){ speaker = driver, text = "Maaf pak, saya tidak membawanya saat ini"}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Dimana SIM Anda?" },
                    new (){ speaker = driver, text = "Uhh.. saya lupa bawa pak"}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Bisa tunjukkan SIM Anda?" },
                    new (){ speaker = driver, text = "Saya belum punya SIM pak"}
                }
            }
        };
    }

    public void InitSTNK(){
        ConvSTNK = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Bisa tunjukkan STNK Anda?" },
                    new (){ speaker = driver, text = "Baik pak, ini STNK saya"}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Anda membawa SNTK?" },
                    new (){ speaker = driver, text = "Iya pak"}
                }
            }
        };
    }

    public void InitNoSTNK(){
        ConvNoSTNK = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Bisa tunjukkan STNK Anda?" },
                    new (){ speaker = driver, text = "Saya tinggalkan di rumah pak"}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Dimana STNK Anda?" },
                    new (){ speaker = driver, text = "Waduh, saya lupa bawa pak, hehe"}
                }
            },
        };
    }

    public void InitVerdictGuilty() {
        ConvVerdictGuilty = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Anda akan dikenakan sanksi karena melanggar aturan lalu lintas" },
                    new (){ speaker = driver, text = "Tolong pak, jangan tilang saya.."}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Mohon maaf, Anda akan diberi sanksi sesuai pasal yang tertera" },
                    new (){ speaker = driver, text = ".."}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Anda terkena sanksi karena melanggar lalu lintas, ini slip Anda" },
                    new (){ speaker = driver, text = "P-pak saya tidak salah apa-apa"}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Lain kali jangan dilakukan lagi, ini slip sanksi Anda" },
                    new (){ speaker = driver, text = "Hm.."}
                }
            },
        };
    }

    public void InitVerdictInnocent() {
        ConvVerdictInnocent = new (){
            new () {
                flow = new (){
                    new (){ speaker = you,    text = "Baik, sepertinya surat-surat Anda sudah lengkap, Anda boleh pergi" },
                    new (){ speaker = driver, text = "Terima kasih pak"}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Maaf mengganggu waktu Anda, Anda boleh pergi" },
                    new (){ speaker = driver, text = "Baik"}
                }
            },
            new() {
                flow = new (){
                    new (){ speaker = you,    text = "Sepertinya surat-surat Anda sudah lengkap semua, silakan melanjutkan perjalanan Anda" },
                    new (){ speaker = driver, text = ".."}
                }
            }
        };
    }

}
