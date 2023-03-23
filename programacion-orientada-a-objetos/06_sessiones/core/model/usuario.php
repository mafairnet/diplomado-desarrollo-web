<?php

class Usuario{

    public $id;
    public $firstName;
    public $lastName;
    public $username;
    public $password;
    
    public function __construct($id,$firstName,$lastName,$username,$password){
        $this->id=$id;
        $this->firstName=$firstName;
        $this->lastName=$lastName;
        $this->username=$username;
        $this->password=$password;
    }

    public function getID(){
        return $this->id;
    }

    public function setId($id){
        $this->id = $id;
    }

    public function getFirstName(){
        return $this->firstName;
    }

    public function setFirstName($firstName){
        $this->firstName = $firstName;
    }
    
    public function getLastName(){
        return $this->lastName;
    }

    public function setLastName($lastName){
        $this->lastName = $lastName;
    }

    public function getUsername(){
        return $this->username;
    }

    public function setUsername($username){
        $this->username = $username;
    }

    public function getPassword(){
        return $this->password;
    }

    public function setPassword($password){
        $this->password = $password;
    }
}

?>