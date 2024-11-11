-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 04, 2024 at 09:03 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `librarydb`
--

-- --------------------------------------------------------

--
-- Table structure for table `books`
--

CREATE TABLE `books` (
  `BookID` int(11) NOT NULL,
  `Title` varchar(255) NOT NULL,
  `Author` varchar(255) NOT NULL,
  `Availability` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `books`
--

INSERT INTO `books` (`BookID`, `Title`, `Author`, `Availability`) VALUES
(1, 'Pride and Prejudice', 'Jane Austen', 0),
(3, 'To kill a Mockingbird', 'Harper Lee', 0),
(5, 'Wuthering Heights', 'Emily Bronte', 1),
(6, 'The Notebook', 'Nicholas Sparks', 0),
(7, 'The Great Gatsby', 'F. Scott Fitzgerald', 1),
(8, 'The Catcher in the Rye', 'J.D. Salinger', 1),
(9, 'Dune', 'Frank Herbert', 1),
(10, 'Harry Potter and the Sorcerer’s Stone', 'J.K. Rowling', 1),
(11, 'The Hobbit', 'J.R.R. Tolkien', 1),
(12, 'All the Light We Cannot See', 'Anthony Doerr', 1),
(13, 'The Nightingale', 'Kristin Hannah', 1),
(14, 'The Pillars of the Earth', 'Ken Follett', 1),
(15, 'Shutter Island', 'Dennis Lehane', 1),
(16, 'Me Before You', 'Jojo Moyes', 1),
(17, 'The Time Traveler\'s Wife', 'Audrey Niffenegger', 1),
(18, 'The Haunting of Hill House', 'Shirley Jackson', 1),
(19, 'Harry Potter and the Deathly Hallows', 'J.K. Rowling', 1),
(20, 'Beloved', 'Toni Morrison', 1),
(21, 'A Brief History of Time', 'Stephen Hawking', 1),
(22, 'The Origin of Species', 'Charles Darwin', 1),
(23, 'The Art of Learning', 'Josh Waitzkin', 1);

-- --------------------------------------------------------

--
-- Table structure for table `borrowing`
--

CREATE TABLE `borrowing` (
  `BorrowID` int(11) NOT NULL,
  `BookID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `BorrowDate` date NOT NULL,
  `ReturnDate` date DEFAULT NULL,
  `Returned` tinyint(1) DEFAULT 0,
  `LateFee` decimal(10,2) DEFAULT 0.00,
  `Name` varchar(255) DEFAULT NULL,
  `BorrowedCount` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `borrowing`
--

INSERT INTO `borrowing` (`BorrowID`, `BookID`, `UserID`, `BorrowDate`, `ReturnDate`, `Returned`, `LateFee`, `Name`, `BorrowedCount`) VALUES
(1, 1, 5, '2024-08-18', '2024-09-01', 0, 0.00, '', 0),
(6, 6, 2, '2024-09-03', '2024-09-10', 0, 0.00, '', 1),
(7, 11, 6, '2024-08-25', '2024-09-01', 1, 0.00, 'Jane', 0);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserID` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `UserRole` enum('Librarian','Member') NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `Username`, `Password`, `UserRole`, `name`) VALUES
(1, 'admin', 'adminpass', 'Librarian', ''),
(2, 'user1', 'user1pass', 'Member', ''),
(5, 'Masha', '1234', 'Member', 'Masha Kidurangi'),
(6, 'Jane123', '123', 'Member', 'Jane'),
(8, 'ruviamaya', 'ruvi123', 'Member', 'Ruvi Amaya'),
(9, 'kevin123', 'kevinpass', 'Member', 'Kevin');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `books`
--
ALTER TABLE `books`
  ADD PRIMARY KEY (`BookID`);

--
-- Indexes for table `borrowing`
--
ALTER TABLE `borrowing`
  ADD PRIMARY KEY (`BorrowID`),
  ADD KEY `BookID` (`BookID`),
  ADD KEY `UserID` (`UserID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`),
  ADD KEY `Username` (`Username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `books`
--
ALTER TABLE `books`
  MODIFY `BookID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT for table `borrowing`
--
ALTER TABLE `borrowing`
  MODIFY `BorrowID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `borrowing`
--
ALTER TABLE `borrowing`
  ADD CONSTRAINT `borrowing_ibfk_1` FOREIGN KEY (`BookID`) REFERENCES `books` (`BookID`),
  ADD CONSTRAINT `borrowing_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
