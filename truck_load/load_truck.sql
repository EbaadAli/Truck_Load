-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 08, 2023 at 06:31 AM
-- Server version: 10.4.25-MariaDB
-- PHP Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `load_truck`
--

-- --------------------------------------------------------

--
-- Table structure for table `box_item`
--

CREATE TABLE `box_item` (
  `boxID` int(32) NOT NULL,
  `PLU` varchar(20) NOT NULL,
  `number` int(6) NOT NULL,
  `weight` decimal(4,0) NOT NULL,
  `status` varchar(1) NOT NULL,
  `truckID` int(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `box_item`
--

INSERT INTO `box_item` (`boxID`, `PLU`, `number`, `weight`, `status`, `truckID`) VALUES
(1, 'BOX_1', 1, '1', 'I', 0),
(2, 'BOX_2', 2, '2', 'I', 0),
(3, 'BOX_3', 3, '2', 'I', 0);

-- --------------------------------------------------------

--
-- Table structure for table `truck`
--

CREATE TABLE `truck` (
  `truckID` int(32) NOT NULL,
  `name` varchar(20) NOT NULL,
  `number` int(6) NOT NULL,
  `status` varchar(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `truck`
--

INSERT INTO `truck` (`truckID`, `name`, `number`, `status`) VALUES
(1, 'Truck A', 0, 'O'),
(2, 'Truck B', 0, 'O'),
(3, 'Truck C', 0, 'O');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `box_item`
--
ALTER TABLE `box_item`
  ADD PRIMARY KEY (`boxID`),
  ADD UNIQUE KEY `boxID` (`boxID`);

--
-- Indexes for table `truck`
--
ALTER TABLE `truck`
  ADD PRIMARY KEY (`truckID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `box_item`
--
ALTER TABLE `box_item`
  MODIFY `boxID` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `truck`
--
ALTER TABLE `truck`
  MODIFY `truckID` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
