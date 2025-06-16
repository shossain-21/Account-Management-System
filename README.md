# Qcounts: A Mini Account Management System

A lightweight, Razor Pages-based accounting system built from scratch using only stored procedures — no LINQ, just clean, deliberate ADO.NET.

## Why I Built This

This project is part of a technical assignment during a job application. The goal was to build a real-world accounting system using ASP.NET Core Razor Pages and SQL Server — but with some tough constraints: no LINQ, and only stored procedures allowed.

While it looked scary at first, I took it as a chance to dive deep into how web apps talk to databases — without shortcuts.

## The Challenges I Faced

Coming from a competitive programming and C# background, I had little to no exposure to Razor Pages or stored procedures. Previously, I had worked with Entity Framework, ADO.NET + LINQ and had some basic knowledge of the .NET Core MVC framework. Working with Razor Pages and Stored Procedures — both being new to me — was initially challenging, but it pushed me to understand deeper aspects of request handling, server-side rendering, and direct database interaction using parameterized queries. Over time, I became comfortable with defining and executing stored procedures, binding data to Razor Pages, and organizing logic in a cleaner, more maintainable way. This experience has significantly expanded my understanding of full-stack development within the .NET ecosystem.

## My Approach

Instead of rushing, I focused on building one feature at a time:

1. **Authentication & Roles** — using ASP.NET Identity and custom roles (Admin, Accountant, Viewer).
2. **Chart of Accounts** — created a dynamic account system using stored procedures (spCreateControl, spCreateSubSidiary) and ADO.NET.
3. **Voucher Module** — built forms to save journal/payment/receipt vouchers in multi-line format.
4. **No LINQ** — all data access is raw ADO.NET with `SqlConnection`, `SqlCommand`, and stored procedures.

Each step was a learning moment, especially wiring dropdowns to SQL and looping through multi-line entries.


## Key Features

- User Roles: Admin, Accountant, Viewer — with custom access.
- Chart of Accounts: Hierarchical parent-child account structure.
- Voucher Entry: Journal, Payment, and Receipt voucher support.
- Stored Procedure Only: With ADO.Net and No LINQ.


## Primary Technologies Used

- ASP.NET Core Razor Pages
- SQL Server + Stored Procedures
- ASP.NET Identity
- Git & GitHub for version control

## Screenshots & UI Overview

### Login Page
![Login Page](screenshots/Login-Page.png)

### Register Page
![Login Page](screenshots/Register-Page.png)


### Landing Page
#### ADMIN
![Admin Landing Page](screenshots/Admin-Landing-Page.png)
![Payment Voucher](screenshots/Admin-Payment-Voucher.png)
![Receipt Voucher](screenshots/Admin-Receipt-Voucher.png)
![Journal Voucher](screenshots/Admin-Journal-Voucher.png)
![Chat of Accounts](screenshots/Admin-COA.png)
![User Settings](screenshots/Admin-UserSettings.png)

#### Accountant
![Accountant Landing Page](screenshots/Acc-Landing-Page.png)

#### Viewer
![Viewer Landing Page](screenshots/Viewer-Landing-Page.png)



### Payment Voucher
#### Add Cash Payment
![Add Cash Payment](screenshots/Cash-Pay-Create.png)

![Edit Cash Payment](screenshots/Cash-Pay-Edit.png)

#### View Cash Payment
![View Cash Payment](screenshots/Cash-Pay-Index.png)

#### Add Bank Payment
![Add Bank Payment](screenshots/Bank-Pay-Create.png)

![Edit Bank Payment](screenshots/Bank-Pay-Edit.png)

#### View Bank Payment
![View Bank Payment](screenshots/Bank-Pay-Index.png)



### Receipt Voucher
#### Add Cash Receipt
![Add Cash Receipt](screenshots/Cash-Rcv-Create.png)

![Edit Cash Receipt](screenshots/Cash-Rcv-Edit.png)

#### View Cash Receipt
![View Cash Receipt](screenshots/Cash-Rcv-Index.png)

#### Add Bank Receipt
![Add Bank Receipt](screenshots/Bank-Rcv-Create.png)

![Edit Bank Receipt](screenshots/Bank-Rcv-Edit.png)

#### View Bank Receipt
![View Bank Receipt](screenshots/Bank-Rcv-Index.png)



### Journal Voucher
#### Add Journal Voucher
![Add Journal Voucher](screenshots/JV-Create.png)

![Edit Journal Voucher](screenshots/JV-Edit.png)

#### View Journal Voucher
![View Journal Voucher](screenshots/JV-Index.png)



### Chart of Accounts
#### Add Control 
![Add Control](screenshots/COA-Control-Create.png)

#### View Controls
![Voucher Entry](screenshots/COA-Control-Index.png)

#### Add Sub-Sidiary
![Voucher Entry](screenshots/COA-SubSidiary-Create.png)

#### View Sub-Sidiary
![Voucher Entry](screenshots/COA-SubSidiary-Index.png)


### User Settings
#### Create User
![Create User](screenshots/User-Create.png)

#### View Users
![View User](screenshots/User-Index.png)



### Export Report 
![Export to Excel](screenshots/Export1.png)

#### File Name (Voucher Type Name + Date_Time).xlsx
![Export to Excel](screenshots/Export2.png)

![Export to Excel](screenshots/Export3.png)