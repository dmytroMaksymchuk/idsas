import {useEffect, useState} from 'react';
import './App.css';
import Header from "./components/Header.tsx";
import UploadFilePopUp from "./components/UploadFilePopUp.tsx";
import MyDocumentsList, {ShareState, MyDocument} from "./components/MyDocumentList.tsx";
import SharedDocumentList, {SharedDocument} from "./components/SharedDocumentList.tsx";

const HTTP_PATH = `http://localhost:32768/`;
function App() {
    const [selectedFile, setSelectedFile] = useState(null);
    const [uploadFileActive, setUploadFileActive] = useState(false);
    const [userIndex, setUserIndex] = useState(0);

    //const usersTokens = [
    //    "579c59ec-f4a1-4acd-8c50-448217bbfdde",
    //    "12e420da-543e-44cb-bdb7-298d33c4ad48",
    //    "05698aa2-fdbf-4a37-a6e1-bf842165f136",
    //]

    const [myDocuments, setMyDocuments] = useState<MyDocument[]>([
        { name: 'Document1.pdf', shareState: ShareState.shared },
        { name: 'Document2.docx', shareState: ShareState.private },
        { name: 'Presentation.pptx', shareState: ShareState.waitingConfirmation },
    ]);
    
    const [sharedDocuments, setSharedDocuments] = useState<SharedDocument[]>([
        { name: 'Document3.pdf', available: true },
        {
            name: 'Document4.docx',
            available: false
        },
        {
            name: 'Presentation2.pptx',
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
        //fetch('api/document/document', {
        //    method: 'GET',
        //})
        //.then((response) => response.json())
        //.then((data) => {
        //    console.log('Documents fetched successfully:', data);
        //    setMyDocuments(data.myDocuments);
        //    setSharedDocuments(data.sharedDocuments);
        //})
        //.catch((error) => {
        //    console.error('Error fetching documents:', error);
        //});
        //console.log('Fetching documents...');
    }


    // Handle file upload (this can be customized to your API or upload logic)
    const handleFileUpload = () => {
        if (selectedFile) {
            // Create a FormData object to send the file to the server
            const formData = new FormData();
            formData.append('file', selectedFile);
            //formData.append('authorToken');

            // Send the file to the server
            fetch(HTTP_PATH + 'api/document/upload', {
                method: 'POST',
                body: formData,
            })
            .then((response) => response.json())
            .then((data) => {
                console.log('File uploaded successfully:', data);
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
        setSelectedFile(null);
    }

    const onAccessSharedFile = (token: string) => {
        console.log('Accessing shared file with token:', token);
    };

    const onConfirmDocument = (index: number) => {
        console.log('Confirming document:', index);
    }

    const onRejectDocument = (index: number) => {
        console.log('Rejecting document:', index);
    }

    const onShareDocument = (index: number) => {
        console.log('Sharing document:', index);
    }

    useEffect(() => {
        // Fetch the documents when the app loads
        fetchDocuments();
    }, []);

    useEffect(() => {
        setUploadFileActive(false);
        fetchDocuments();
    }, [userIndex]);

    return (
        <div className="App">
            <Header uploadFileActive={uploadFileActive} setUploadFileActive={setUploadFileActive} userIndex={userIndex} setUserIndex={onUserChange} />
            {uploadFileActive &&
                <UploadFilePopUp handleFileChange={handleFileChange} onSign={handleSignDocument} setUploadFileActive={setUploadFileActive}/>
            }
            {!uploadFileActive &&
                <>
                <img className={"refresh-button"} src={"./refresh.svg"} alt={"refresh"} onClick={fetchDocuments} />
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
