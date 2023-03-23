<?php

//include "../../core/config/db.php";

$db_server   = $GLOBALS['db_server'];
$db_username = $GLOBALS['db_username'];
$db_password = $GLOBALS['db_password'];
$db_name     = $GLOBALS['db_name'];

function userDao($job,$id,$data){
    
    global $db_server,$db_username,$db_password,$db_name;

    if( $job == "get_all" ||
        $job == "get" ||
        $job == "add" ||
        $job == "edit" ||
        $job == "delete"
    ){
        if (isset($id)){
            if (!is_numeric($id)){
                $id = 0;
            }
        }
    } else {
        $job = "";
    }

    $mysql_data = array();
    $result  = 'N/A';
    $message = 'N/A';

    if($job != ""){
        
        $db_connection = mysqli_connect($db_server, $db_username, $db_password, $db_name);

        if (mysqli_connect_errno()){
            $result  = 'error';
            $message = 'Failed to connect to database: ' . mysqli_connect_error();
        } else {

            if ($job == 'get_all')
            {
                $query = "SELECT * FROM user ORDER BY id";
                $query = mysqli_query($db_connection, $query);
                
                if (!$query)
                {
                    $result  = 'error';
                    $message = 'query error';
                }
                else
                {
                    $result  = 'success';
                    $message = 'query success';
                    while ($sqldata = mysqli_fetch_array($query))
                    {
                        $mysql_data[] = array(
                            "id" => $sqldata['id'],
                            "first_name"  => $sqldata['first_name'],
                            "second_name"  => $sqldata['second_name'],
                            "username"  => $sqldata['username'],
                            "password"  => $sqldata['password']
                        );
                    }
                }
            }

            if ($job == 'get'){
        
                // Get extension
                if ($id == 0){
                    $result  = 'error';
                    $message = 'id missing';
                } else {
                    $query = "SELECT * from user where id = " . mysqli_real_escape_string($db_connection,$id);
                    
                    $query = mysqli_query($db_connection, $query);
                    
                    if (!$query)
                    {
                        $result  = 'error';
                        $message = 'query error';
                    }
                    else
                    {
                        $result  = 'success';
                        $message = 'query success';
                        while ($sqldata = mysqli_fetch_array($query))
                        {
                            $mysql_data[] = array(
                                "id" => $sqldata['id'],
                                "first_name"  => $sqldata['first_name'],
                                "second_name"  => $sqldata['second_name'],
                                "username"  => $sqldata['username'],
                                "password"  => $sqldata['password']
                            );
                        }
                    }
                }
            }

            if ($job == 'add'){
        
                // Add 
                $query = "INSERT INTO user SET ";
                
                if ($data['first_name'] != "") { $query .= "first_name = '" . mysqli_real_escape_string($db_connection, $data['first_name']) . "', "; }
                if ($data['second_name'] != "") { $query .= "second_name = '" . mysqli_real_escape_string($db_connection, $data['second_name']) . "', "; }
                if ($data['username'] != "") { $query .= "username = '" . mysqli_real_escape_string($db_connection, $data['username']) . "', "; }
                if ($data['password'] != "") { $query .= "password = '" . md5(mysqli_real_escape_string($db_connection, $data['password'])) . "'"; }
                
                //die ($query);
                
                $query = mysqli_query($db_connection, $query);
                
                if (!$query){
                    $result  = 'error';
                    $message = 'query error';
                } else {
                    $result  = 'success';
                    $message = 'query success';
                }
            }

            if ($job == 'edit'){
        
                // Edit 
                if ($id == 0){
                    $result  = 'error';
                    $message = 'id missing';
                } else {
                    
                    $query = "UPDATE user SET ";
                    
                    if ($data['first_name'] != "") { $query .= "first_name = '" . mysqli_real_escape_string($db_connection, $data['first_name']) . "', "; }
                    if ($data['second_name'] != "") { $query .= "second_name = '" . mysqli_real_escape_string($db_connection, $data['second_name']) . "', "; }
                    if ($data['username'] != "") { $query .= "username = '" . mysqli_real_escape_string($db_connection, $data['username']) . "', "; }
                    if ($data['password'] != "") { $query .= "password = '" . md5(mysqli_real_escape_string($db_connection, $data['password'])) . "'"; }
                    
                    $query .= "WHERE id = '" . mysqli_real_escape_string($db_connection, $id) . "'";
                    //die($query);
                    $query  = mysqli_query($db_connection, $query);
                    
                    if (!$query){
                        $result  = 'error';
                        $message = 'query error';
                    } else {
                        $result  = 'success';
                        $message = 'query success';
                    }
                }
            }

            if ($job == 'delete')
            {
            
                // Delete 
                if ($id == 0){
                    $result  = 'error';
                    $message = 'id missing';
                } else {
                    
                    $query = "DELETE from user WHERE id = '" . mysqli_real_escape_string($db_connection, $id) . "'";
                    $query = mysqli_query($db_connection, $query);
                    
                    if (!$query){
                        $result  = 'error';
                        $message = 'query error';
                    } else {
                        $result  = 'success';
                        $message = 'query success';
                    }
                }
            }
   
        }

        mysqli_close($db_connection);

    }

    if(!isset($mysql_data)){$mysql_data = array();}

    $data = array(
        "result"  => $result,
        "message" => $message,
        "data"    => $mysql_data
    );

    return $data;
}

?>