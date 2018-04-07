<?php
require "conn.php";

if (isset($_POST['portfolioname']) && isset($_POST['currentuser'])){

$mPortfolioName = $_POST['portfolioname'];
$mUsername = $_POST['currentuser'];

$sql = 'DELETE FROM Portfolios WHERE portfolioname = "'.$mPortfolioName.'" LIMIT 1';
if (mysqli_query($conn, $sql)) {
echo "Portfolios deleted successfully";
} else {
echo "Error deleting record: " . $conn->error;
}

$sql2 = 'DELETE FROM UsersPortfolios WHERE portfolioname = "'.$mPortfolioName.'" AND username = "'.$mUsername.'" LIMIT 1';
if (mysqli_query($conn, $sql2)) {
echo "UsersPortfolios deleted successfully";
} else {
echo "Error deleting record: " . $conn->error;
}

$sql3 = 'DELETE FROM PortfoliosInvestments WHERE portfolioname = "'.$mPortfolioName.'" LIMIT 1';

if (mysqli_query($conn, $sql3)) {
echo "Record deleted successfully";
} else {
echo "Error deleting record: " . $conn->error;
}


mysqli_close($conn);

}else{
	echo 'unable to identify insert content';
}
?>