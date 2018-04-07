<?php
require "conn.php";

if (isset($_POST['portfolioname']) && isset($_POST['tickersymbol'])){

$mPortfolioName = $_POST['portfolioname'];
$mTicker = $_POST['tickersymbol'];

$sql = 'DELETE FROM PortfoliosInvestments WHERE portfolioname = "'.$mPortfolioName.'" AND tickersymbol = "'.$mTicker.'" LIMIT 1';

if (mysqli_query($conn, $sql)) {
echo "Record deleted successfully";
} else {
echo "Error deleting record: " . $conn->error;
}

mysqli_close($conn);

}else{
	echo 'unable to identify insert content';
}
?>