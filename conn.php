<?php

$dbhost = 'oniddb.cws.oregonstate.edu';
$dbname = 'jonesty-db';
$dbuser = 'jonesty-db';
$dbpass = 'HYv5EsultqYNRbP0';

$conn = mysqli_connect($dbhost, $dbuser, $dbpass, $dbname);
if($conn){
}
else{
echo "connection error\n";
}

?>