// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function adminLoadView(status, user) {
    var apiUrl = '/login/defaultview';
    if (status ==="authview")
        apiUrl = '/adminLogin/authview';
    if (status === "profileview")
        apiUrl = '/adminMain/profileview';
    if (status === "usersView")
        apiUrl = '/adminMain/usersView';
    if (status === "transactionview") 
        apiUrl = '/adminMain/transactionview';
    if (status === "userEditView")
        apiUrl = '/adminMain/userEditView';
   

    console.log("Hello " + apiUrl +"admin");

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

           // document.getElementById("contentFrame").innerHTML = data;

            if (status === "authview") {
                document.getElementById("userNameDisplay").innerHTML = "admin";
             

            }
            if (status === "profileview") {
                document.getElementById("userNameDisplay").innerHTML = "admin";
                updateAdminnfo(user);
     

            } if (status === "transactionview") {
                document.getElementById("userNameDisplay").innerHTML = "admin";
              
                displayAdminTransactions();

            }
          
            if (status === "userEditView") {
                document.getElementById("userNameDisplay").innerHTML = "admin";
                updateUserInfo(user);


            }

            if (status === "usersView") {
                document.getElementById("userNameDisplay").innerHTML = "admin";

                displayAllUsers();

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

    const apiInfoUrl = '/api/userprofile/Username/' + username;

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

function updateAdminnfo(username) {


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


         /*   AccNum = userProfile.accountNumber;
            console.log('Acc no.:', userProfile.accountNumber);
            document.getElementById("AccountNumberContainer").innerHTML = userProfile.accountNumber;
*/

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

           // updateAccBalance(userProfile.accountNumber)
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

function getAdminTransactions(username) {

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

function displayAdminTransactions() {

    //  var name = document.getElementById('userN').value;
    // var password = document.getElementById('pass').value;


    const apiTransactionUrl = 'api/Bank/GetAllTransactions';


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
            tableHTML += '<thead><tr><th>Transaction ID</th><th>Transaction Type</th><th>From/To Account</th><th>Amount</th><th>Description</th><th>Date</th></tr></thead>';
            tableHTML += '<tbody>';

            transactions.forEach(transaction => {
                
                tableHTML += '<tr>';
                tableHTML += `<td>${transaction.transactionId}</td>`;
                tableHTML += `<td>${transaction.transactionType}</td>`;
                tableHTML += `<td>${transaction.accountNumber}</td>`;
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
            //console.log('transactions:', tableHTML);
            // Update the 'list-container' with the HTML table
            document.getElementById('transaction_container').innerHTML = tableHTML;
           
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });

}

function displayAllUsers() {

    //  var name = document.getElementById('userN').value;
    // var password = document.getElementById('pass').value;


    const apiTransactionUrl = 'api/Bank/GetAllUserProfiles';


    fetch(apiTransactionUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json(); // Assuming the response is in JSON format
        })
        .then(users => {
            // Create an HTML table to display the transactions
            let tableHTML = '<table>';
            tableHTML += '<thead><tr><th>User ID</th><th>Username</th><th>Full Name</th><th>Account Number</th><th>Edit User</th></tr></thead>';
            tableHTML += '<tbody>';

            users.forEach(user => {

                tableHTML += '<tr>';
                tableHTML += `<td>${user.userId}</td>`;
                tableHTML += `<td>${user.username}</td>`;
                tableHTML += `<td>${user.fullName}</td>`;
                tableHTML += `<td>${user.accountNumber}</td>`;

               // console.log('transactions:', user.username);
                tableHTML += `<td><button class="edit_but" onclick="adminLoadView('userEditView','${user.username}')">Edit</button></td>`;
                tableHTML += '</tr>';
            });

            tableHTML += '</tbody>';
            tableHTML += '</table>';
            //console.log('transactions:', tableHTML);
            // Update the 'list-container' with the HTML table
            document.getElementById('users_container').innerHTML = tableHTML;

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



/*
const loginButton = document.getElementById('LoginButton');
loginButton.addEventListener('click', loadView);

const aboutButton = document.getElementById('AboutButton');
aboutButton.addEventListener('click', loadView("about"));

const logoutButton = document.getElementById('LogoutButton');
logoutButton.addEventListener('click', loadView("logout"));*/
