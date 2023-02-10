using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserProfileResponse
{
   
    public int id;
    public string user_id;
    public string first_Name;
    public string last_name;
    public string phone_number;
    public string profile_picture;
    public string homeaddress;
    public string date_of_birth;
    public string gender;
    public string sessionid;
    public string valid_till;

    // [{"id":5,"user_id":"a81e166c461abb18d79f771fa443b4007ee36df6e3da7ba0666af70d02a3f066","first_Name":"not_stated","last_name":"not_stated",
    // "phone_number":"not_stated","profile_picture":"no_photo",
    // "homeaddress":"not_home","date_of_birth":"1970-01-01T00:00:00.000Z","gender":"not_stated",
    // "sessionid":"da8fd678cfde4f62d1d79159147a5bf3d21ab7f8c664ea0bb02a89cd3226995a1d17210470628648798495a76dec0d72ad844a4baa5bb5ebbccff3355be38a9e",
    // "valid_till":"2023-02-02T12:32:58.000Z"}]

}
