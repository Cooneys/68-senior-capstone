<?php
require "conn.php";

if (isset($_POST['portfolioname']) && isset($_POST['currentuser']) && isset($_POST['totalvalue'])){

$mPortfolioName = $_POST['portfolioname'];
$mUser = $_POST['currentuser'];
$mValue = $_POST['totalvalue'];

$sql = 'UPDATE UsersPortfoliosView SET totalvalue = "'.$mValue.'" WHERE portfolioname = "'.$mPortfolioName.'" AND username = "'.$mUser.'"';

if (mysqli_query($conn, $sql)) {
echo "Value Updated Correctly";
} else {
echo "Error adding value: " . $conn->error;
}

mysqli_close($conn);

}else{
	echo 'unable to identify insert content';
}
?>
