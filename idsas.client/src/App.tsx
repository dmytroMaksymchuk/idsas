import {useEffect, useState} from 'react';
import './App.css';
import Header from "./components/Header.tsx";
import UploadFilePopUp from "./components/UploadFilePopUp.tsx";
import MyDocumentsList, {ShareState, MyDocument} from "./components/MyDocumentList.tsx";
import SharedDocumentList, {SharedDocument} from "./components/SharedDocumentList.tsx";

const HTTP_PATH = `http://localhost:5000/`;
function App() {
    const [selectedFile, setSelectedFile] = useState(null);
    const [uploadFileActive, setUploadFileActive] = useState(false);
    const [userIndex, setUserIndex] = useState(0);

    const usersTokens = [
        "579c59ec-f4a1-4acd-8c50-448217bbfdde",
        "12e420da-543e-44cb-bdb7-298d33c4ad48",
        "05698aa2-fdbf-4a37-a6e1-bf842165f136",
    ]

    const [myDocuments, setMyDocuments] = useState<MyDocument[]>([
        { name: 'Document1.pdf', token: '123', shareState: ShareState.shared },
        { name: 'Document2.docx', token: '1234', shareState: ShareState.private },
        { name: 'Presentation.pptx', token: '1235', shareState: ShareState.waitingConfirmation },
    ]);
    
    const [sharedDocuments, setSharedDocuments] = useState<SharedDocument[]>([
        { name: 'Document3.pdf', linkToken: "123", available: true },
        {
            name: 'Document4.docx',
            linkToken: "123", 
            available: false
        },
        {
            name: 'Presentation2.pptx',
            linkToken: "123", 
            available: false
        },
    ]);

    const handleSignDocument = () => {
        console.log('Signing document');
        setUploadFileActive(false);
        handleFileUpload();
    }
    const handleFileChange = (event: any) => {
        setSelectedFile(event.target.files[0]);
    };
    const onDownloadFile = (index: number) => {
        console.log('Downloading file:', index);
    };
    const fetchDocuments = () => {
        // Fetch the documents from the server
        fetch(HTTP_PATH + `api/document/all?userToken=${usersTokens[userIndex]}`, {
            method: 'GET',
        })
        .then((response) => response.json())
        .then((data) => {
            console.log('Documents fetched successfully:', data);
            let documents: MyDocument[] = [];
            data.forEach((doc: any) => {
                let shareState = ShareState.private;
                console.log(doc.shareState)
                if (doc.shareState == 2) {
                    shareState = ShareState.waitingConfirmation;
                }
                documents.push({ name: doc.title, token: doc.documentToken, shareState: shareState  })
            });

            console.log(documents)

            setMyDocuments(documents);
        })
        .catch((error) => {
            console.error('Error fetching documents:', error);
        });
        console.log('Fetching documents...');
    }
    const fetchSharedDocuments = () => {
        // Fetch the documents from the server
        fetch(HTTP_PATH + `api/document/sharedWithMe?userToken=${usersTokens[userIndex]}`, {
            method: 'GET',
        })
        .then((response) => response.json())
        .then((data) => {
            console.log('Shared Documents fetched successfully:', data);
            let documents: SharedDocument[] = [];
            data.forEach((doc: any) => {
                documents.push({ name: doc.title, linkToken: doc.documentToken, available: doc.available, })
            });

            setSharedDocuments(documents);
        })
        .catch((error) => {
            console.error('Error fetching documents:', error);
        });
        console.log('Fetching documents...');
    }


    // Handle file upload (this can be customized to your API or upload logic)
    const handleFileUpload = () => {
        if (selectedFile) {
            // Create a FormData object to send the file to the server
            const formData = new FormData();
            formData.append('file', selectedFile);

            // Send the file to the server
            fetch(HTTP_PATH + `api/document/upload?authorToken=${usersTokens[userIndex]}`, {
                method: 'POST',
                body: formData
            })
            .then((response) => response.json())
            .then((data) => {
                console.log('File uploaded successfully:', data);
                fetchDocuments();
            })
            .catch((error) => {
                console.error('Error uploading file:', error);
            });
        } else {
            console.log('No file selected');
        }
    };

    const onUserChange = (index: number) => {
        setUserIndex(index);
        fetchDocuments();
        fetchSharedDocuments();
        setSelectedFile(null);
    }

    const onAccessSharedFile = (token: string) => {
        // Fetch the documents from the server
        fetch(HTTP_PATH + `api/document/access?documentToken=${token}&userToken=${usersTokens[userIndex]}`, {
            method: 'POST',
        })
        .then(() => {
            console.log('succesfully accessed document');
            fetchSharedDocuments();
        })
        .catch((error) => {
            console.error('Error accessing document:', error);
        });
        console.log('Accessing document...');
        fetchSharedDocuments();
    };

    const onConfirmDocument = (index: number) => {
        fetch(HTTP_PATH + `api/link/allowAccess?linkToken=${myDocuments[index].token}&userToken=${usersTokens[userIndex]}`, {
            method: 'POST',
        })
        .then(() => {
            console.log('succesfully allowed access to document');
            fetchSharedDocuments();
        })
        .catch((error) => {
            console.error('Error allowing access to document:', error);
        });
        console.log('Allowing access to document...');
    }

    const onRejectDocument = (index: number) => {
        fetch(HTTP_PATH + `api/link/denyAccess?linkToken=${myDocuments[index].token}&userToken=${usersTokens[userIndex]}`, {
            method: 'POST',
        })
        .then(() => {
            console.log('succesfully not allowed access to document');
            fetchSharedDocuments();
        })
        .catch((error) => {
            console.error('Error not allowing access to document:', error);
        });
        console.log('Not allowing access to document...');
    }

    const onShareDocument = (index: number) => {
        fetch(HTTP_PATH + `api/document/share?documentToken=${myDocuments[index].token}&userToken=${usersTokens[userIndex]}`, {
            method: 'POST',
        })
        .then((response) => {
            return response.text(); // Parse as text instead of JSON
        })
        .then((data) => {
            console.log('Shared document ID: ' + data);
            navigator.clipboard.writeText(data)
            console.log('succesfully shared document');
            fetchSharedDocuments();
        })
        .catch((error) => {
            console.error('Error sharing document:', error);
        });
        console.log('Sharing document...');
    }

    const fetchAll = () => {
        fetchDocuments();
        fetchSharedDocuments();
    }

    useEffect(() => {
        // Fetch the documents when the app loads
        fetchDocuments();
        fetchSharedDocuments();
    }, []);

    useEffect(() => {
        setUploadFileActive(false);
        fetchDocuments();
        fetchSharedDocuments();
    }, [userIndex]);



    return (
        <div className="App">
            <Header uploadFileActive={uploadFileActive} setUploadFileActive={setUploadFileActive} userIndex={userIndex} setUserIndex={onUserChange} />
            {uploadFileActive &&
                <UploadFilePopUp handleFileChange={handleFileChange} onSign={handleSignDocument} setUploadFileActive={setUploadFileActive}/>
            }
            {!uploadFileActive &&
                <>
                <img className={"refresh-button"} src={"./refresh.svg"} alt={"refresh"} onClick={fetchAll} />
                <MyDocumentsList title={"My Documents"} documents={myDocuments}
                                 onDownloadFile={onDownloadFile} confirmDocument={onConfirmDocument}
                                 rejectDocument={onRejectDocument} onShareDocument={onShareDocument}/>
                    <SharedDocumentList title={"Documents Shared With Me"} documents={sharedDocuments}
                                        onDownloadFile={() => console.log("Download")} onFileAccess={onAccessSharedFile}/>
                </>
            }
        </div>
    );
}

export default App;
