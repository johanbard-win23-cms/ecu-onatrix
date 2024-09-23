// VARIABLES
const errorMessages = {
    name: 'Must be at least one character',
    email: 'Must be a valid email address',
    phone: 'Must be a valid phone number',
}

const defaultMessages = {
    name: '',
    email: '',
    phone: '',
}

const formInputs = {
    name: document.querySelector('#name'),
    email: document.querySelector('#email'),
    phone: document.querySelector('#phone'),
}

var errorArray = []

// EVENT LISTENERS
document.querySelector('body').addEventListener('mousedown', (e) => {
    for (const key in formInputs) {
        if (formInputs[key] != null) {
            let el = `${formInputs[key].id}`
            document.querySelector(`#${el}`).classList.remove('shake')
        }
    }
})

function handleSubmit(e) {
    e.preventDefault()
    
    errorArray = []

    for (const key in formInputs ) {
        if (formInputs[key] != null) {
            if (formInputs[key].hasAttribute('required')) { 
                ifEmpty(formInputs[key]) 
            }
            validationSwitch(formInputs[key])
        }
    }
    
    if (!errorArray.includes(true) ) {
        let formValues

        if (document.querySelector('#name') != null && document.querySelector('#phone') != null && document.querySelector('#contact-target') != null) {
            formValues = {
                name: document.querySelector('#name').value,
                email: document.querySelector('#email').value,
                phone: document.querySelector('#phone').value,
                category: document.querySelector('#contact-target').value
            }
        }
        else {
            formValues = {
                email: document.querySelector('#email').value
            }
        }
        post(formValues)
    }
}

// VALIDATE FUNCTIONS
function ifEmpty(inp) {
    if (inp.value == "") {
        error(inp, true)
    }
    else {
        error(inp, false)
    }
}

function validationSwitch(inp) {
    switch(inp.id) {
        case 'name':
            const nameRegex = /^[a-zA-Z\s\-]+$/
            error(inp, !nameRegex.test(inp.value))
            break

        case 'email':
            const emailRegex = /(([^<>()\[\]\\.,:\s@"]+(\.[^<>()\[\]\\.,:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))/
            error(inp, !emailRegex.test(inp.value))
            break
        default:
          break
      }
}

// ERROR HANDLING
function error(inp, add) {
    const target = document.querySelector(`#${inp.id}`)
    const errorSpan = target.parentNode.querySelector('span');

    if (add) {
        target.classList.add('error')
        target.classList.add('shake')
        if (errorSpan != null)
            errorSpan.innerHTML = errorMessages[inp.id]
        errorArray.push(true)
    }
    else {
        target.classList.remove('error')
        if (errorSpan != null)
            errorSpan.innerHTML = defaultMessages[inp.id]
    }
}

// SEND TO API
//const apiUrl = "https://jb-onatrix-contactprovider.azurewebsites.net/api/CreateContactRequest?code=qHTqm6obHf3IzdHKj1xKHN2KJfYnNdFJKDGVU-Qszw2sAzFuMVXQ3Q%3D%3D"

//async function post(contactObj) {
//    const contactJson = JSON.stringify(contactObj)

//    try {
//        const result = await fetch(`${apiUrl}`, {
//            method: "post",
//            headers: {
//                "Content-type": "application/json"
//            },
//            body: contactJson
//        })
//        if (result.ok) {
//            console.log(`${result.status} : ${result.statusText}`)
//            location.reload()
//        }
//        else
//            console.log(`ERROR : ${result.status} : ${result.statusText}`)
//    }
//    catch(err) {
//        console.log('ERROR : ', err)
//    }
//}

// SEND TO SURFACE CONTROLLER
//const apiUrl = "/umbraco/surface/contactsurface/handlejsonsubmit"
//function post(contactObj) {
//    const contactJson = JSON.stringify(contactObj)

//    console.log(contactJson)


//    fetch(`${apiUrl}`, {
//        method: 'POST',
//        headers: {
//            "Content-type": 'application/json'
//        },
//        body: contactJson
//    })
//    .then(response => response.json())
//    .then(data => {
//        console.log('Success:', data);
//    })
//    .catch((err) => {
//        console.log('Error:', err);
//    });
//}