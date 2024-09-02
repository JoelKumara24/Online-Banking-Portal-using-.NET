// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function loadView(status, user) {
    var apiUrl = '/login/defaultview';
    if (status ==="authview")
        apiUrl = '/login/authview';
    if (status === "profileview")
        apiUrl = '/main/profileview';
    if (status === "accountview")
        apiUrl = '/main/accountview';
    if (status === "transactionview") 
        apiUrl = '/main/transactionview';
    if (status === "transferview")
        apiUrl = '/main/transferview';
    if (status === "logout")
        apiUrl = '/api/logout';

    console.log("Hello "+apiUrl +user);

    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text();
        })
        .then(data => {
            // Handle the data from the API
            document.documentElement.innerHTML = data;

            if (status === "authview") {
                document.getElementById("userNameDisplay").innerHTML = user;
             

            }
            if (status === "profileview") {
                document.getElementById("userNameDisplay").innerHTML = user;
                updateUserInfo(user);
     

            } if (status === "transactionview") {
                document.getElementById("userNameDisplay").innerHTML = user;
              
                getTransactions(user);

            }
            if (status === "transferview") {
                document.getElementById("userNameDisplay").innerHTML = user;

                UpdateTransferPage(user);

            }

            if (status === "accountview") {
                document.getElementById("userNameDisplay").innerHTML = user;

                UpdateAccDetailsPage(user);

            }
           
        })
        .catch(error => {
            // Handle any errors that occurred during the fetch
            console.error('Fetch error:', error);
        });

}


function performAuth() {

    var name = document.getElementById('userN').value;
    var password = document.getElementById('pass').value;
 
    const apiUrl = '/login/auth/' + name + '/' + password;




    fetch(apiUrl)
        .then(response => {
            if (response.ok) {
                if (name == "admin") {

                    adminLoadView("authview", name);

                } else {
                    loadView("authview", name);
                }
            }

            else {
                alert('Incorrect login details!');
                loadView("defaultview", name);
                console.error('Password is incorrect.');
            }
        })
       
        .catch(error => {
            // Handle any errors that occurred during the fetch
            console.error('Fetch error:', error);
        });


}

function updateUserInfo(username) {


    document.getElementById("imageInput").addEventListener("change", function (event) {
        var imgElement = document.getElementById("prof_img");
        var fileInput = event.target;

        if (fileInput.files.length > 0) {
            var selectedFile = fileInput.files[0];
            var objectURL = URL.createObjectURL(selectedFile);

            // Update the src attribute of the img element with the selected image
            imgElement.src = objectURL;
        }
    });
  //  var name = document.getElementById('userN').value;
   // var password = document.getElementById('pass').value;
   
    const apiInfoUrl = '/api/userprofile/Username/' + username ;

    var AccNum;

    fetch(apiInfoUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json(); // Assuming the response is in JSON format
        })
        .then(userProfile => {



         
            console.log('userId.:', userProfile.userId);
            document.getElementById("userIDContainer").innerHTML = userProfile.userId;


            AccNum = userProfile.accountNumber;
            console.log('Acc no.:', userProfile.accountNumber);
            document.getElementById("AccountNumberContainer").innerHTML = userProfile.accountNumber;


            // You can now access attribute data from the userProfile object
            console.log('fullName:', userProfile.fullName);
            document.getElementById("ProfileNameContainer").innerHTML = userProfile.fullName;

          

            console.log('Email:', userProfile.email);
            document.getElementById("ProfileEmailContainer").innerHTML = userProfile.email;

            console.log('Phone:', userProfile.phone);
            document.getElementById("ProfilePhoneNoContainer").innerHTML = userProfile.phone;


            console.log('address:', userProfile.address);
            document.getElementById("ProfileAddressContainer").innerHTML = userProfile.address;



            console.log('userName:', userProfile.username);
            document.getElementById("ProfileUserNameContainer").innerHTML = userProfile.username;



            console.log('Password:', userProfile.passwordHash);
            document.getElementById("ProfilePasswordContainer").innerHTML = userProfile.passwordHash;



            console.log('img:', userProfile.profilePictureUrl);
            document.getElementById("prof_img").src = userProfile.profilePictureUrl;

          //  getProfImg(userProfile.userId);
       

            // ... and so on

            updateAccBalance(userProfile.accountNumber)
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
        


  

}
function UpdateAccDetailsPage(username) {
    const apiDetailsUrl = '/api/userprofile/Username/' + username;



    fetch(apiDetailsUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json(); // Assuming the response is in JSON format
        })
        .then(userProfile => {




            console.log('userId.:', userProfile.userId);
            document.getElementById("userIDContainer").innerHTML = userProfile.userId;


            console.log('Acc no.:', userProfile.accountNumber);
            document.getElementById("AccountNumberContainer").innerHTML = userProfile.accountNumber;

            console.log('fullName:', userProfile.fullName);
            document.getElementById("ProfileNameContainer").innerHTML = userProfile.fullName;


            updateAccBalance(userProfile.accountNumber)

           
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });

}

function updateAccBalance(AccNumber) {

    //  var name = document.getElementById('userN').value;
    // var password = document.getElementById('pass').value;


    const apiBalUrl = '/api/account/' + AccNumber;


    fetch(apiBalUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json(); // Assuming the response is in JSON format
        })
        .then(Account => {
            // You can now access attribute data from the userProfile object
            
            document.getElementById("AccountBalanceContainer").innerHTML = Account.balance;
            console.log('Acc balance updated:', Account.balance);


        })
        .catch(error => {
            console.error('Fetch error:', error);
        });

}


function getTransactions(username) {

    const apiInfoUrl = '/api/userprofile/Username/' + username;

 

    fetch(apiInfoUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json(); // Assuming the response is in JSON format
        })
        .then(userProfile => {




            console.log('userId.:', userProfile.userId);
            document.getElementById("userIDContainer").innerHTML = userProfile.userId;


            console.log('Acc no.:', userProfile.accountNumber);
            document.getElementById("AccountNumberContainer").innerHTML = userProfile.accountNumber;




            updateAccBalance(userProfile.accountNumber)

            displayTransactions(userProfile.accountNumber);
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });

}

function displayTransactions(AccNumber) {

    //  var name = document.getElementById('userN').value;
    // var password = document.getElementById('pass').value;


    const apiTransactionUrl = '/api/transaction/History/' + AccNumber;


    fetch(apiTransactionUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json(); // Assuming the response is in JSON format
        })
        .then(transactions => {
            // Create an HTML table to display the transactions
            let tableHTML = '<table>';
            tableHTML += '<thead><tr><th>Transaction ID</th><th>Transaction Type</th><th>Amount</th><th>Description</th><th>Date</th></tr></thead>';
            tableHTML += '<tbody>';

            transactions.forEach(transaction => {
                tableHTML += '<tr>';
                tableHTML += `<td>${transaction.transactionId}</td>`;
                tableHTML += `<td>${transaction.transactionType}</td>`;
                tableHTML += `<td>${transaction.amount}</td>`;
                tableHTML += `<td>${transaction.description}</td>`;
                // You can format the date as needed, e.g., using moment.js or Date() constructor
              //  const transactionDate = new Date(transaction.transactionDate);
                const transactionDate = new Date(transaction.transactionDate);
                const formattedDate = transactionDate.toLocaleString();
                tableHTML += `<td>${formattedDate}</td>`;
                tableHTML += '</tr>';
            });

            tableHTML += '</tbody>';
            tableHTML += '</table>';
            console.log('transactions:', tableHTML);
            // Update the 'list-container' with the HTML table
            document.getElementById('transaction_container').innerHTML = tableHTML;
           
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });

}

function displayAdvancedUserSettings() {

    if (document.getElementById("ProfileUserNameContainer").style.display == "block") {

        document.getElementById("ProfileUserNameContainer").style.display = "none";
        document.getElementById("UserNameLabel").style.display = "none";
        document.getElementById("PasswordLabel").style.display = "none";
        document.getElementById("ProfilePasswordContainer").style.display = "none";

    } else {
        document.getElementById("ProfileUserNameContainer").style.display = "block";
        document.getElementById("UserNameLabel").style.display = "block";
        document.getElementById("PasswordLabel").style.display = "block";
        document.getElementById("ProfilePasswordContainer").style.display = "block";
    }
}

function updateDatabase() {

    const userId = document.getElementById('userIDContainer').textContent;

    const username = document.getElementById('ProfileUserNameContainer').textContent;

    const passwordHash = document.getElementById('ProfilePasswordContainer').textContent;

    const fullName = document.getElementById('ProfileNameContainer').textContent;

    const email = document.getElementById('ProfileEmailContainer').textContent;

    const phone = document.getElementById('ProfilePhoneNoContainer').textContent;

    const address = document.getElementById('ProfileAddressContainer').textContent;
    const imgData = document.getElementById('prof_img').src;

    const accountNumber = document.getElementById('AccountNumberContainer').textContent;

    const userData = {
        userId: userId,
        username: username,
        passwordHash: passwordHash,
        fullName: fullName,
        email: email,
        phone: phone,
        profilePictureUrl: imgData,
        accountNumber: accountNumber,
        address: address

    };

    const apiUrlUpdate = '/api/UserProfile'; // Replace with the actual endpoint URL

    fetch(apiUrlUpdate, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    })
        .then(response => {
            if (response.ok) {
                alert('Profile Updated');
                return response.json(); // Assuming the response contains the created user data
            } else {
                throw new Error('Network response was not ok');
            }
        })

        .catch(error => {
            console.error('Fetch error:', error);
        });

}
// Example usage:
// In your HTML, you should have input elements with IDs: username, name, accountName, and address.
// Call the createUser function to send the data to the API.
function UpdateTransferPage(username) {
    const apiTransferInfoUrl = '/api/userprofile/Username/' + username;

    fetch(apiTransferInfoUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json(); // Assuming the response is in JSON format
        })
        .then(userProfile => {




            console.log('userId.:', userProfile.userId);
            document.getElementById("userIDContainer").innerHTML = userProfile.userId;


            console.log('Acc no.:', userProfile.accountNumber);
            document.getElementById("AccountNumberContainer").innerHTML = userProfile.accountNumber;


            updateAccBalance(userProfile.accountNumber);

         
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });



}


function transferButClicked() {

    const toAccNum = document.getElementById('TransferToAccountNumberContainer').textContent;
    const fromAccNum = document.getElementById('AccountNumberContainer').textContent;

    const amount = document.getElementById('AmountContainer').textContent;
    const bal = document.getElementById('AccountBalanceContainer').textContent;

    const description = document.getElementById('DescriptionContainer').textContent;

    const balancefl = parseFloat(bal); // Convert balance to a double
    const withdrawalAmount = parseFloat(amount); 
    

    if (balancefl >= withdrawalAmount) {
        withdraw(fromAccNum, toAccNum, amount, description);
    }else {
    alert('Sorry Insufficient Account balance for the transaction!');
}

   
   
 

  
}

function deposit(toAccNum, amount, description) {




    const depositdetails = {
        accountNumber: toAccNum,
        amount: amount,
        description: description
    
    };

    const apiUrlDeposit = '/api/transaction/deposit'; // Replace with the actual endpoint URL



    fetch(apiUrlDeposit, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(depositdetails)
    })
        .then(response => {
            if (response.ok) {
              //  alert('Transfer Successfull!');
                console.log('Deposit successful!:', depositdetails);
                alert('Transfer Successfull!');
                // return response.json(); // Assuming the response contains the created user data
            } else {
                throw new Error('Network response was not ok');
            }
        })

        .catch(error => {
            console.error('Fetch error:', error);
        });

  

}


function withdraw(fromAccNum, toAccNum, amount, description) {



  
        const withdrawDetails = {
            accountNumber: fromAccNum,
            amount: amount,
            description: description


        };

        const apiUrlWithdraw = '/api/transaction/Withdraw';

   
        fetch(apiUrlWithdraw, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(withdrawDetails)
        })
            .then(response => {
                if (response.ok) {
                    deposit(toAccNum, amount, description);
                 
                    console.log('withdraw successful!:', withdrawDetails);
                    updateAccBalance(fromAccNum);
                    // return response.json(); // Assuming the response contains the created user data
                } else {
                    throw new Error('Network response was not ok');
                }
            })

            .catch(error => {
                console.error('Fetch error:', error);
            });


}
function selectImage() {
    // Trigger the file input element when the button is clicked
    document.getElementById("imageInput").click();
}

// Add an event listener to the file input element to handle image selection

function getUserFromElement() {

    return document.getElementById('userNameDisplay').textContent;
  
}


document.addEventListener("DOMContentLoaded", loadView);
/*
const loginButton = document.getElementById('LoginButton');
loginButton.addEventListener('click', loadView);

const aboutButton = document.getElementById('AboutButton');
aboutButton.addEventListener('click', loadView("about"));

const logoutButton = document.getElementById('LogoutButton');
logoutButton.addEventListener('click', loadView("logout"));*/
