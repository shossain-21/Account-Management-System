# Qcounts: A Mini Account Management System

A lightweight, Razor Pages-based accounting system built from scratch using only stored procedures — no LINQ, just clean, deliberate ADO.NET.

## Why I Built This

This project is part of a technical assignment during a job application. The goal was to build a real-world accounting system using ASP.NET Core Razor Pages and SQL Server — but with some tough constraints: no LINQ, and only stored procedures allowed.

While it looked scary at first, I took it as a chance to dive deep into how web apps talk to databases — without shortcuts.

## The Challenges I Faced

Coming from a competitive programming and C# background, I had little to no exposure to Razor Pages or stored procedures. I didn’t know how to structure pages, handle user roles, or even build forms that talk to SQL. 

Honestly, I felt lost at first — but I started breaking the problem down one piece at a time.

## My Approach

Instead of rushing, I focused on building one feature at a time:

1. **Authentication & Roles** — using ASP.NET Identity and custom roles (Admin, Accountant, Viewer).
2. **Chart of Accounts** — created a dynamic account system using a stored procedure and ADO.NET.
3. **Voucher Module** — built forms to save journal/payment/receipt vouchers in multi-line format.
4. **No LINQ** — all data access is raw ADO.NET with `SqlConnection`, `SqlCommand`, and stored procedures.

Each step was a learning moment, especially wiring dropdowns to SQL and looping through multi-line entries.


## Key Features

- User Roles: Admin, Accountant, Viewer — with custom access.
- Chart of Accounts: Hierarchical parent-child account structure.
- Voucher Entry: Journal, Payment, and Receipt voucher support.
- Stored Procedure Only: No LINQ.


## Primary Technologies Used

- ASP.NET Core Razor Pages
- SQL Server + Stored Procedures
- ASP.NET Identity
- Git & GitHub for version control

## Screenshots & UI Overview

### Login Page
![Login Page (Sample Image)](screenshots/Login-Page.png)

### Register Page
![Login Page (Sample Image)](screenshots/Register-Page.png)

### Landing Page
#### ADMIN
![Admin Landing Page (Sample Image)](screenshots/Admin-Landing-Page.png)
![Payment Voucher (Sample Image)](screenshots/Admin-Payment-Voucher.png)
![Receipt Voucher (Sample Image)](screenshots/Admin-Receipt-Voucher.png)
![Journal Voucher (Sample Image)](screenshots/Admin-Journal-Voucher.png)
![Chat of Accounts (Sample Image)](screenshots/Admin-COA.png)
![User Settings (Sample Image)](screenshots/Admin-UserSettings.png)

#### Accountant
![Accountant Landing Page (Sample Image)](screenshots/Acc-Landing-Page.png)

#### Viewer
![Viewer Landing Page (Sample Image)](screenshots/Viewer-Landing-Page.png)


### Payment Voucher
#### Add Cash Payment
![Add Cash Payment (Sample Image)](screenshots/Cash-Pay-Create.png)

![Edit Cash Payment (Sample Image)](screenshots/Cash-Pay-Edit.png)

#### View Cash Payment
![View Cash Payment (Sample Image)](screenshots/Cash-Pay-Index.png)

#### Add Bank Payment
![Add Bank Payment (Sample Image)](screenshots/Bank-Pay-Create.png)

![Edit Bank Payment (Sample Image)](screenshots/Bank-Pay-Edit.png)

#### View Bank Payment
![View Bank Payment (Sample Image)](screenshots/Bank-Pay-Index.png)



### Receipt Voucher
#### Add Cash Receipt
![Add Cash Receipt (Sample Image)](screenshots/Cash-Rcv-Create.png)

![Edit Cash Receipt (Sample Image)](screenshots/Cash-Rcv-Edit.png)

#### View Cash Receipt
![View Cash Receipt (Sample Image)](screenshots/Cash-Rcv-Index.png)

#### Add Bank Receipt
![Add Bank Receipt (Sample Image)](screenshots/Bank-Rcv-Create.png)

![Edit Bank Receipt (Sample Image)](screenshots/Bank-Rcv-Edit.png)

#### View Bank Receipt
![View Bank Receipt (Sample Image)](screenshots/Bank-Rcv-Index.png)


### Journal Voucher
#### Add Journal Voucher
![Add Journal Voucher (Sample Image)](screenshots/JV-Create.png)

![Edit Journal Voucher (Sample Image)](screenshots/JV-Edit.png)

#### View Journal Voucher
![View Journal Voucher (Sample Image)](screenshots/JV-Index.png)

### Chart of Accounts
![COA (Sample Image)](screenshots/chart-of-accounts-page.png)

### Voucher Entry
![Voucher Entry (Sample Image)](screenshots/voucher-entry-page.png)