-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 15, 2022 at 01:22 PM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `rest_api_demo`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_bill_info` (IN `invoiceId` VARCHAR(20))  SELECT billId,billTo,billAmount,billCurrency,billNumber,dueDate,status,partialPayAllowed,description
	 FROM mytable1 WHERE billId = invoiceId$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Pay_Bill_Notification` (IN `invoiceId` VARCHAR(50), IN `paidBy` VARCHAR(50), IN `paidAt` VARCHAR(50), IN `paidDate` VARCHAR(50), IN `tansactionId` VARCHAR(50), IN `amount` VARCHAR(50), IN `currency` VARCHAR(50))  BEGIN
INSERT INTO rest_api_demo.mytable1(billId,billTo,billNumber,paidAt,dueDate,tansactionId,billAmount,billCurrency)
		VALUES(invoiceId,paidBy,paidBy,paidAt,paidDate,tansactionId,amount,currency);
		SELECT CONCAT(invoiceId, paidDate) confirmationId;
        
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `billinfo`
--
-- Error reading structure for table rest_api_demo.billinfo: #1932 - Table &#039;rest_api_demo.billinfo&#039; doesn&#039;t exist in engine
-- Error reading data for table rest_api_demo.billinfo: #1064 - You have an error in your SQL syntax; check the manual that corresponds to your MariaDB server version for the right syntax to use near &#039;FROM `rest_api_demo`.`billinfo`&#039; at line 1

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `id` int(11) NOT NULL,
  `name` varchar(256) NOT NULL,
  `description` text NOT NULL,
  `price` int(255) NOT NULL,
  `category_id` int(11) NOT NULL,
  `created` datetime NOT NULL,
  `modified` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`id`, `name`, `description`, `price`, `category_id`, `created`, `modified`) VALUES
(1, 'LG P880 4X HD', 'My first awesome phone!', 336, 3, '2014-06-01 01:12:26', '2014-05-31 14:42:26'),
(2, 'Google Nexus 4', 'The most awesome phone of 2013!', 299, 2, '2014-06-01 01:12:26', '2014-05-31 14:42:26'),
(3, 'Samsung Galaxy S4', 'How about no?', 600, 3, '2014-06-01 01:12:26', '2014-05-31 14:42:26'),
(6, 'Bench Shirt', 'The best shirt!', 29, 1, '2014-06-01 01:12:26', '2014-05-30 23:42:21'),
(7, 'Lenovo Laptop', 'My business partner.', 399, 2, '2014-06-01 01:13:45', '2014-05-30 23:43:39'),
(8, 'Samsung Galaxy Tab 10.1', 'Good tablet.', 259, 2, '2014-06-01 01:14:13', '2014-05-30 23:44:08'),
(9, 'Spalding Watch', 'My sports watch.', 199, 1, '2014-06-01 01:18:36', '2014-05-30 23:48:31'),
(10, 'Sony Smart Watch', 'The coolest smart watch!', 300, 2, '2014-06-06 17:10:01', '2014-06-05 15:39:51'),
(11, 'Huawei Y300', 'For testing purposes.', 100, 2, '2014-06-06 17:11:04', '2014-06-05 15:40:54'),
(12, 'Abercrombie Lake Arnold Shirt', 'Perfect as gift!', 60, 1, '2014-06-06 17:12:21', '2014-06-05 15:42:11'),
(13, 'Abercrombie Allen Brook Shirt', 'Cool red shirt!', 70, 1, '2014-06-06 17:12:59', '2014-06-05 15:42:49'),
(26, 'Another product', 'Awesome product!', 555, 2, '2014-11-22 19:07:34', '2014-11-21 18:37:34'),
(28, 'Wallet', 'You can absolutely use this one!', 799, 6, '2014-12-04 21:12:03', '2014-12-03 20:42:03'),
(31, 'Amanda Waller Shirt', 'New awesome shirt!', 333, 1, '2014-12-13 00:52:54', '2014-12-12 00:22:54'),
(42, 'Nike Shoes for Men', 'Nike Shoes', 12999, 3, '2015-12-12 06:47:08', '2015-12-12 04:17:08'),
(48, 'Bristol Shoes', 'Awesome shoes.', 999, 5, '2016-01-08 06:36:37', '2016-01-08 04:06:37'),
(60, 'Rolex Watch', 'Luxury watch.', 25000, 1, '2016-01-11 15:46:02', '2016-01-11 13:16:02');

-- --------------------------------------------------------

--
-- Table structure for table `mytable`
--
-- Error reading structure for table rest_api_demo.mytable: #1932 - Table &#039;rest_api_demo.mytable&#039; doesn&#039;t exist in engine
-- Error reading data for table rest_api_demo.mytable: #1064 - You have an error in your SQL syntax; check the manual that corresponds to your MariaDB server version for the right syntax to use near &#039;FROM `rest_api_demo`.`mytable`&#039; at line 1

-- --------------------------------------------------------

--
-- Table structure for table `mytable1`
--

CREATE TABLE `mytable1` (
  `ID` int(11) NOT NULL,
  `billId` varchar(7) NOT NULL,
  `billTo` varchar(22) NOT NULL,
  `billAmount` decimal(3,1) NOT NULL,
  `billCurrency` varchar(3) NOT NULL,
  `billNumber` varchar(11) NOT NULL,
  `paidAt` varchar(4) NOT NULL,
  `dueDate` varchar(19) NOT NULL,
  `status` varchar(7) DEFAULT 'pending',
  `partialPayAllowed` varchar(50) DEFAULT NULL,
  `description` varchar(21) DEFAULT 'ict biller',
  `tansactionId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mytable1`
--

INSERT INTO `mytable1` (`ID`, `billId`, `billTo`, `billAmount`, `billCurrency`, `billNumber`, `paidAt`, `dueDate`, `status`, `partialPayAllowed`, `description`, `tansactionId`) VALUES
(2, 'C113031', '252614755640', '0.1', 'USD', '25261475564', 'BANK', '2020-02-30T15:40:55', 'pending', '1', 'ict biller', 2147483647),
(3, 'C113032', '252614755640', '0.1', 'USD', '25261475564', 'BANK', '2020-02-30T15:40:55', 'pending', '1', 'ict biller', 2147483647),
(4, 'C113033', '252614755670', '0.3', 'USD', '25261475567', 'BANK', '2020-02-30T15:40:55', 'pending', '1', 'ict biller', 2147483647),
(5, 'C113030', '252615339350', '0.1', 'SOS', '25261533935', 'MMT', '2022-02-15T14:36:57', 'pending', NULL, 'ict biller', 2147483647),
(8, 'C113032', '252615339350', '0.1', 'SOS', '25261533935', 'MMT', '2022-02-15T15:06:47', 'pending', NULL, 'ict biller', 2147483647);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--
-- Error reading structure for table rest_api_demo.users: #1932 - Table &#039;rest_api_demo.users&#039; doesn&#039;t exist in engine
-- Error reading data for table rest_api_demo.users: #1064 - You have an error in your SQL syntax; check the manual that corresponds to your MariaDB server version for the right syntax to use near &#039;FROM `rest_api_demo`.`users`&#039; at line 1

--
-- Indexes for dumped tables
--

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `mytable1`
--
ALTER TABLE `mytable1`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;

--
-- AUTO_INCREMENT for table `mytable1`
--
ALTER TABLE `mytable1`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
