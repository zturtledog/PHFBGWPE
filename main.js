var commands = [["hi"],["embed"],["help"],["reference"]];
var command_value = [0,1,2,3,4];

require('dotenv').config();

const { debug } = require('console');
const { SSL_OP_NO_SESSION_RESUMPTION_ON_RENEGOTIATION } = require('constants');
const Discord = require('discord.js');
const { Client, MessageEmbed } = require('discord.js');
const client = new Discord.Client();
client.login(process.env.TOKEN);

client.on('ready', readyDiscord);

function readyDiscord() {
    console.log('ready');
}

client.on('message', gotMessage);

function event(message,type)
{
    console.log("Event."+type+" :: "+message);
}

function generateRandomColor()
{
    var randomColor = '#'+Math.floor(Math.random()*16777215).toString(16);

    event(randomColor,"Generator.color_generated")

    return randomColor;
    //random color will be freshly served
}

function gotMessage(msg) {
    // console.log(msg.content);

    var info = msg.content.split("");
    var value = msg.content;

    if (info[0] == "!") {
        for (let i = 0; i < commands.length; i++) {
            prosses_commands(compare_commands(commands[i],value,command_value[i]),msg);
        }
    }
}

function compare_commands(kEy,message,value) {
    var info = message.split(" ");
    var end = [];


    if (info[1] == kEy[0]) {  
        end[0] = value;  
        end[1] = info[2]
        end[2] = info[3]
    }

    return(end);  
}

function decode(value) {
    var info = value.split("_");
    var end = "";
    let endi = 0;

    if (info.length>1) {
        for (let i = 0; i < info.length-1; i++) {
            end += info[i]+" ";
            endi = i;
        }
        end += info[endi+1];
    }else{
        end = info[0];
    }

    return(end);
}

function prosses_commands(input,msg) {
    var UUID_ = msg.author;
    var channel = msg.channel
    var value = input[0];

    if (value == 0) {//hello
        make_embed("bot","hello!",generateRandomColor(),channel);
    }else if (value == 1 && input.length>1) {
        delete_msg(msg);
        make_embed(msg.author.username+" says:","",generateRandomColor(),channel);
        make_embed(decode(input[1]),decode(input[2]),generateRandomColor(),channel);
    }else if (value == 2) {
        delete_msg(msg);
        make_embed("help page","use '! hi' to say hi. \nuse '! embed <title> <content>' to make an embed (use '_' instead of ' ' in title and content) \nuse '! reference <unity thing>' to get a link to the scripting reference",generateRandomColor(),channel);
    }else if (value == 3 && input.length>0) {
        delete_msg(msg);
        make_embed("reference","https://docs.unity3d.com/ScriptReference/30_search.html?q="+input[1],generateRandomColor(),channel)
    }else if (value == 4) {

    }else if (value == 5) {
        
    }else if (value == 6) {
        
    }else if (value == 7) {
        
    }else if (value == 8) {
        
    }else if (value == 9) {
        
    }else if (value == 10) {
        
    }else if (value == 12) {
        
    }else if (value == 13) {
        
    }else if (value == 14) {
        
    }
}

function make_embed(title,discription,colors,channels) {
    const embed = new MessageEmbed()
      .setTitle(title)
      .setColor(colors)
      .setDescription(discription);
    channels.send(embed);
}

function delete_msg(msg) {
    msg.delete({timeout:10})
        .then(msg => console.log(`Deleted message from ${msg.author.username} after 5 seconds`))
        .catch(console.error);
}

function runcmd(cmd,channel) {
    const { exec } = require('child_process');
    exec(cmd, (err) => {
    if (err) {
        make_embed("System","an error was encountered : "+err,generateRandomColor(),channel)
        return;
    }});
}